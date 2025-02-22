using System.Collections.Concurrent;
using System.Diagnostics;
using Avalonia.Media.Imaging;
using PicView.Avalonia.ImageHandling;
using PicView.Core.ImageDecoding;
using static System.GC;

namespace PicView.Avalonia.Preloading;

/// <summary>
///     The PreLoader class is responsible for preloading images asynchronously and caching them.
/// </summary>
public class PreLoader : IAsyncDisposable
{
#if DEBUG

    // ReSharper disable once ConvertToConstant.Local
    private static readonly bool ShowAddRemove = true;
#endif

    /// <summary>
    ///     Indicates whether the preloader is currently running.
    /// </summary>
    private static bool _isRunning;

    private readonly PreLoaderConfig _config = new();

    private readonly Lock _lock = new();

    private readonly ConcurrentDictionary<int, PreLoadValue> _preLoadList = new();

    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    ///     Gets the maximum count of preloaded images.
    /// </summary>
    public static int MaxCount => PreLoaderConfig.MaxCount;

    #region Add

    /// <summary>
    ///     Adds an image to the preload list asynchronously.
    /// </summary>
    /// <param name="index">The index of the image in the list.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if the image was added successfully; otherwise, false.</returns>
    public async Task<bool> AddAsync(int index, List<string> list)
    {
        if (list == null || index < 0 || index >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(AddAsync)} invalid parameters: \n{index}");
#endif
            return false;
        }

        if (_preLoadList.ContainsKey(index))
        {
            return false;
        }

        var imageModel = new ImageModel();

        var preLoadValue = new PreLoadValue(imageModel);
        if (!_preLoadList.TryAdd(index, preLoadValue))
        {
            return false;
        }

        preLoadValue.IsLoading = true;
        try
        {
            var fileInfo = imageModel.FileInfo = new FileInfo(list[index]);
            imageModel = await GetImageModel.GetImageModelAsync(fileInfo).ConfigureAwait(false);
            preLoadValue.ImageModel = imageModel;
            imageModel.EXIFOrientation ??= EXIFHelper.GetImageOrientation(fileInfo);

#if DEBUG
            if (ShowAddRemove)
            {
                Trace.WriteLine($"{imageModel?.FileInfo?.Name} added at {index}");
            }
#endif
            return true;
        }
        catch (Exception ex)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(AddAsync)} exception: \n{ex}");
#endif
            return false;
        }
        finally
        {
            preLoadValue.IsLoading = false;
        }
    }

    public bool Add(int index, List<string> list, ImageModel imageModel)
    {
        if (list == null || index < 0 || index >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(Add)} invalid parameters: \n{index}");
#endif
            return false;
        }

        if (imageModel is null)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(Add)} invalid ImageModel");
#endif
            return false;
        }

        var preLoadValue = new PreLoadValue(imageModel);
        return _preLoadList.TryAdd(index, preLoadValue);
    }

    #endregion

    #region Refresh and resynchronize

    public void RefreshFileInfo(int index, FileInfo fileInfo, List<string> list)
    {
        if (list == null)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshFileInfo)} list null \n{index}");
#endif
            return;
        }

        if (index < 0 || index >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshFileInfo)} invalid index: \n{index}");
#endif
            return;
        }

        var isExisting = _preLoadList.TryGetValue(index, out var value);
        if (!isExisting)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshFileInfo)} index not found: \n{index}");
#endif
            return;
        }

        value.ImageModel.FileInfo = fileInfo;
    }

    /// <summary>
    ///     Resynchronizes the preload list with the given list of image paths.
    /// </summary>
    /// <param name="list">The list of image paths.</param>
    /// <remarks>
    ///     Call it after the file watcher detects changes, or the list is resorted
    /// </remarks>
    public async Task ResynchronizeAsync(List<string> list)
    {
        if (list == null)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(ResynchronizeAsync)} list is null");
#endif
            return;
        }

        while (_isRunning)
        {
            await _cancellationTokenSource?.CancelAsync();
            await Task.Delay(100);
        }

        // Create a reverse lookup from file path to current index
        var reverseLookup = new Dictionary<string, int>(list.Count);
        for (var i = 0; i < list.Count; i++)
        {
            reverseLookup[list[i]] = i;
        }

        // Snapshot of current keys to avoid modification during iteration
        var keys = _preLoadList.Keys.ToArray();

        foreach (var oldIndex in keys)
        {
            if (!_preLoadList.TryGetValue(oldIndex, out var preLoadValue))
            {
                continue;
            }

            var filePath = preLoadValue.ImageModel?.FileInfo?.FullName;
            if (string.IsNullOrEmpty(filePath))
            {
                Remove(oldIndex, list);
                continue;
            }

            if (!reverseLookup.TryGetValue(filePath, out var newIndex))
            {
                // File no longer exists in the list
                Remove(oldIndex, list);
                continue;
            }

            if (newIndex == oldIndex)
            {
                // Index is unchanged, no action needed
                continue;
            }

            if (newIndex < 0 || newIndex >= list.Count)
            {
                // Invalid new index, remove the entry
                Remove(oldIndex, list);
                continue;
            }

            // Attempt to move the entry to the new index
            if (_preLoadList.TryRemove(oldIndex, out var removedValue))
            {
                if (!_preLoadList.TryAdd(newIndex, removedValue))
                {
#if DEBUG
                    if (ShowAddRemove)
                    {
                        Trace.WriteLine($"Failed to resynchronize {filePath} to index {newIndex}");
                    }
#endif
                }
#if DEBUG
                else if (ShowAddRemove)
                {
                    Trace.WriteLine($"Resynchronized {filePath} from index {oldIndex} to {newIndex}");
                }
#endif
            }
        }
    }

    #endregion

    #region Get

    /// <summary>
    ///     Gets the preloaded value for a specific key.
    /// </summary>
    /// <param name="key">The key of the preloaded value.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>The preloaded value if it exists; otherwise, null.</returns>
    public PreLoadValue? Get(int key, List<string> list)
    {
        if (list != null && key >= 0 && key < list.Count)
        {
            return Contains(key, list) ? _preLoadList[key] : null;
        }
#if DEBUG
        Trace.WriteLine($"{nameof(PreLoader)}.{nameof(Get)} invalid parameters: \n{key}");
#endif
        return null;
    }

    public PreLoadValue? Get(string fileName, List<string> list) =>
        Get(_preLoadList.Values.ToList().FindIndex(x => x.ImageModel.FileInfo.FullName == fileName), list);


    /// <summary>
    ///     Gets the preloaded value for a specific key asynchronously.
    /// </summary>
    /// <param name="key">The key of the preloaded value.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>The preloaded value if it exists; otherwise, null.</returns>
    public async Task<PreLoadValue?> GetAsync(int key, List<string> list)
    {
        if (list == null || key < 0 || key >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(GetAsync)} invalid parameters: \n{key}");
#endif
            return null;
        }

        if (Contains(key, list))
        {
            return _preLoadList[key];
        }

        await AddAsync(key, list);
        return Get(key, list);
    }

    public async Task<PreLoadValue?> GetAsync(string fileName, List<string> list) =>
        await GetAsync(_preLoadList.Values.ToList().FindIndex(x => x.ImageModel?.FileInfo?.FullName == fileName),
            list);

    #endregion

    #region Contains

    /// <summary>
    ///     Checks if a specific key exists in the preload list.
    /// </summary>
    /// <param name="key">The key to check.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if the key exists; otherwise, false.</returns>
    public bool Contains(int key, List<string> list)
    {
        return list != null && key >= 0 && key < list.Count && _preLoadList.ContainsKey(key);
    }

    /// <summary>
    ///     Checks if an image with the specified file name exists in the preload list.
    /// </summary>
    /// <param name="fileName">The full path of the image file to check for existence.</param>
    /// <returns>True if the image exists in the preload list; otherwise, false.</returns>
    public bool Contains(string fileName) =>
        _preLoadList.Values.ToList().FindIndex(x => x.ImageModel.FileInfo.FullName == fileName) != -1;

    #endregion

    #region Remove and clear

    /// <summary>
    ///     Removes a specific key from the preload list.
    /// </summary>
    /// <param name="key">The key to remove.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if the key was removed; otherwise, false.</returns>
    public bool Remove(int key, List<string> list)
    {
        if (list == null || key < 0 || key >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(Remove)} invalid parameters: \n{key}");
#endif
            return false;
        }

        if (!Contains(key, list))
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(Remove)} key does not exist: \n{key}");
#endif
            return false;
        }

        try
        {
            if (_preLoadList.TryGetValue(key, out var item))
            {
                lock (_lock)
                {
                    if (item.ImageModel?.Image is Bitmap bitmap)
                    {
                        bitmap.Dispose();
                    }
                }

                if (item.ImageModel is not null)
                {
                    item.ImageModel.FileInfo = null;
                }

                var removed = _preLoadList.TryRemove(key, out _);
#if DEBUG
                if (removed && ShowAddRemove)
                {
                    Trace.WriteLine($"{list[key]} removed at {list.IndexOf(list[key])}");
                }
#endif
                return removed;
            }
        }
        catch (Exception e)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(Remove)} exception:\n{e.Message}");
#endif
        }

        return false;
    }

    /// <summary>
    ///     Removes an image from the preload list.
    /// </summary>
    /// <param name="fileName">The full path of the image to remove.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if the image was successfully removed; otherwise, false.</returns>
    public bool Remove(string fileName, List<string> list)
    {
        var index = _preLoadList.Values.ToList().FindIndex(x => x.ImageModel.FileInfo.FullName == fileName);
        return Remove(index, list);
    }

    /// <summary>
    ///     Clears all preloaded images from the cache.
    /// </summary>
    public void Clear()
    {
        _cancellationTokenSource?.Cancel();
        foreach (var item in _preLoadList.Values)
        {
            if (item.ImageModel?.Image is Bitmap img)
            {
                img.Dispose();
            }
        }

        _preLoadList.Clear();
    }

    /// <summary>
    ///     Clears all preloaded images from the cache asynchronously.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         This method is used to clear the preloaded images when the image list changes.
    ///     </para>
    ///     <para>
    ///         It is an asynchronous version of the <see cref="Clear" /> method.
    ///     </para>
    /// </remarks>
    public async Task ClearAsync()
    {
        try
        {
            if (_cancellationTokenSource is not null)
            {
                await _cancellationTokenSource?.CancelAsync();
            }
        }
        catch (Exception e)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(ClearAsync)} exception: \n{e.StackTrace}");
#endif
        }

        Clear();
    }

    #endregion

    #region Preload

    /// <summary>
    ///     Preloads images asynchronously.
    /// </summary>
    /// <param name="currentIndex">The current index of the image.</param>
    /// <param name="reverse">Indicates whether to preload in reverse order.</param>
    /// <param name="list">The list of image paths.</param>
    public async Task PreLoadAsync(int currentIndex, bool reverse, List<string> list)
    {
        if (list == null)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(PreLoadAsync)} list null \n{currentIndex}");
#endif
            return;
        }

        if (_isRunning)
        {
            return;
        }

#if DEBUG
        if (ShowAddRemove)
        {
            Trace.WriteLine($"\nPreLoading started at {currentIndex}\n");
        }
#endif

        _cancellationTokenSource ??= new CancellationTokenSource();

        try
        {
            await PreLoadInternalAsync(currentIndex, reverse, list, _cancellationTokenSource.Token)
                .ConfigureAwait(false);
        }
        catch (Exception exception)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoadAsync)} exception:\n{exception.Message}");
#endif
        }
    }

    private async Task PreLoadInternalAsync(int currentIndex, bool reverse, List<string> list,
        CancellationToken token)
    {
        _isRunning = true;

        var count = list.Count;

        int nextStartingIndex, prevStartingIndex;
        if (reverse)
        {
            nextStartingIndex = (currentIndex - 1 + count) % count;
            prevStartingIndex = currentIndex + 1;
        }
        else
        {
            nextStartingIndex = (currentIndex + 1) % count;
            prevStartingIndex = currentIndex - 1;
        }

        var isPrettymuchEmpty = _preLoadList.Count <= 1;
        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = _config.MaxParallelism,
            CancellationToken = token
        };

        try
        {
            if (reverse)
            {
                await LoopAsync(options, false);
                await LoopAsync(options, true);
            }
            else
            {
                await LoopAsync(options, true);
                await LoopAsync(options, false);
            }

            if (!isPrettymuchEmpty)
            {
                RemoveLoop();
            }
        }
        finally
        {
            _isRunning = false;
        }

        return;

        async Task LoopAsync(ParallelOptions parallelOptions, bool positive)
        {
            await Parallel.ForAsync(0, PreLoaderConfig.PositiveIterations, parallelOptions, async (i, _) =>
            {
                var index = positive ? (nextStartingIndex + i) % count : (prevStartingIndex - i + count) % count;
                await AddAsync(index, list);
            });
        }

        void RemoveLoop()
        {
            // Remove items outside the preload range
            if (list.Count <= PreLoaderConfig.MaxCount + PreLoaderConfig.NegativeIterations ||
                _preLoadList.Count <= PreLoaderConfig.MaxCount)
            {
                return;
            }

            while (_preLoadList.Count > PreLoaderConfig.MaxCount)
            {
                Remove(reverse ? _preLoadList.Keys.Max() : _preLoadList.Keys.Min(), list);
            }
        }
    }

    #endregion

    #region IDisposable

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            Clear();
        }

        _disposed = true;
    }

    public async ValueTask DisposeAsync()
    {
        if (_disposed)
        {
            return;
        }

        if (_cancellationTokenSource is not null)
        {
            await ClearAsync().ConfigureAwait(false);
        }

        Dispose(false);
        SuppressFinalize(this);
    }

    ~PreLoader()
    {
        Dispose(false);
    }

    #endregion
}