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
public sealed class PreLoader : IAsyncDisposable
{
#if DEBUG

    // ReSharper disable once ConvertToConstant.Local
    private static readonly bool ShowAddRemove = true;
#endif

    private readonly PreLoaderConfig _config = new();

    private readonly ConcurrentDictionary<int, PreLoadValue> _preLoadList = new();

    private CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    ///     Indicates whether the preloader is currently running.
    /// </summary>
    public static bool IsRunning { get; private set; }

    /// <summary>
    ///     Gets the maximum count of preloaded images.
    /// </summary>
    public static int MaxCount => PreLoaderConfig.MaxCount;

    /// <summary>
    ///     Adds an image to the preload list asynchronously.
    /// </summary>
    /// <param name="index">The index of the image in the list.</param>
    /// <param name="list">The list of image paths.</param>
    /// <param name="imageModel">Optional image model to be added.</param>
    /// <returns>True if the image was added successfully; otherwise, false.</returns>
    public async Task<bool> AddAsync(int index, List<string> list, ImageModel? imageModel = null)
    {
        if (list == null || index < 0 || index >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(AddAsync)} invalid parameters: \n{index}");
#endif
            return false;
        }

        var preLoadValue = new PreLoadValue(imageModel);
        if (!_preLoadList.TryAdd(index, preLoadValue))
            return false;

        preLoadValue.IsLoading = true;
        try
        {
            if (imageModel?.Image == null)
            {
                var fileInfo = imageModel?.FileInfo ?? new FileInfo(list[index]);
                imageModel = await GetImageModel.GetImageModelAsync(fileInfo).ConfigureAwait(false);
            }

            preLoadValue.ImageModel = imageModel;
            imageModel.EXIFOrientation ??= EXIFHelper.GetImageOrientation(imageModel.FileInfo);

#if DEBUG
            if (ShowAddRemove)
                Trace.WriteLine($"{imageModel?.FileInfo?.Name} added at {index}");
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

    /// <summary>
    ///     Refreshes the file information for a specific image in the preload list asynchronously.
    /// </summary>
    /// <param name="index">The index of the image in the list.</param>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if the image was refreshed successfully; otherwise, false.</returns>
    public async Task<bool> RefreshFileInfo(int index, List<string> list)
    {
        if (list == null)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshFileInfo)} list null \n{index}");
#endif
            return false;
        }

        if (index < 0 || index >= list.Count)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshFileInfo)} invalid index: \n{index}");
#endif
            return false;
        }

        var removed = _preLoadList.TryRemove(index, out var preLoadValue);
        if (preLoadValue is null)
        {
            return removed;
        }

        if (preLoadValue.ImageModel != null)
        {
            preLoadValue.ImageModel.FileInfo = null;
        }

        await AddAsync(index, list, preLoadValue.ImageModel).ConfigureAwait(false);
        return removed;
    }

    /// <summary>
    ///     Refreshes the file information for all images in the preload list.
    /// </summary>
    /// <param name="list">The list of image paths.</param>
    /// <returns>True if any image information was successfully updated; otherwise, false.</returns>
    /// <remarks>
    ///     This method iterates over the preload list, updates the file information for each image,
    ///     and attempts to remove and re-add them in the preload list using their updated file paths.
    ///     If an exception occurs, it is caught and logged in debug mode.
    /// </remarks>
    public bool RefreshAllFileInfo(List<string> list)
    {
        if (list == null) return false;

        try
        {
            foreach (var item in _preLoadList)
            {
                // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                if (item.Value?.ImageModel == null) continue;

                item.Value.ImageModel.FileInfo = new FileInfo(list[item.Key]);
                if (!_preLoadList.TryRemove(item.Key, out var newItem)) continue;

                _preLoadList.TryAdd(list.IndexOf(newItem.ImageModel.FileInfo.FullName), newItem);
            }

            return true;
        }
        catch (Exception e)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(PreLoader)}.{nameof(RefreshAllFileInfo)} exception: \n{e.Message}");
#endif
            return false;
        }
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

        if (Contains(key, list)) return _preLoadList[key];

        await AddAsync(key, list);
        return Get(key, list);
    }

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
                if (item.ImageModel?.Image is Bitmap bitmap)
                {
                    bitmap.Dispose();
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

        if (IsRunning)
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
            await PreLoadInternalAsync(currentIndex, reverse, list, _cancellationTokenSource.Token).ConfigureAwait(false);
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
        IsRunning = true;

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

        var options = new ParallelOptions
        {
            MaxDegreeOfParallelism = _config.MaxParallelism,
            CancellationToken = token
        };
        
        var isParallel = _preLoadList.Count <= 1;

        try
        {
            if (reverse)
            {
                await LoopAsync(options, false, isParallel);
                await LoopAsync(options, true, isParallel);
            }
            else
            {
                await LoopAsync(options, true, isParallel);
                await LoopAsync(options, false, isParallel);
            }

            if (!isParallel)
            {
                RemoveLoop();
            }
        }
        finally
        {
            IsRunning = false;
        }

        return;
            
        async Task LoopAsync(ParallelOptions parallelOptions, bool positive, bool parallel)
        {
            if (parallel)
            {
                await Parallel.ForAsync(0, PreLoaderConfig.PositiveIterations, parallelOptions, async (i, _) =>
                {
                    var index = positive ? (nextStartingIndex + i) % count : (prevStartingIndex - i + count) % count;
                    await AddAsync(index, list);
                });
            }
            else
            {
                for (var i = 0; i < PreLoaderConfig.PositiveIterations; i++)
                {
                    var index = positive ? (nextStartingIndex + i) % count : (prevStartingIndex - i + count) % count;
                    await AddAsync(index, list);
                }
            }
        }

        void RemoveLoop()
        {
            // Remove items outside the preload range
            if (list.Count <= PreLoaderConfig.MaxCount + PreLoaderConfig.NegativeIterations ||
                _preLoadList.Count <= PreLoaderConfig.MaxCount)
            {
                return;
            }

            do
            {
                Remove(reverse ? _preLoadList.Keys.Max() : _preLoadList.Keys.Min(), list);
            } while (_preLoadList.Count > PreLoaderConfig.MaxCount);
        }
    }


    #region IDisposable

    private bool _disposed;

    public void Dispose()
    {
        Dispose(true);
        Collect(MaxGeneration, GCCollectionMode.Optimized, false);
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