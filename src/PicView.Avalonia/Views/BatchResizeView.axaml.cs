using Avalonia.Controls;
using Avalonia.Threading;
using ImageMagick;
using PicView.Avalonia.FileSystem;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;
using PicView.Core.FileHandling;
using PicView.Core.ImageDecoding;
using PicView.Core.Localization;

namespace PicView.Avalonia.Views;

public partial class BatchResizeView : UserControl
{
    private bool _isKeepingAspectRatio = true;
    private bool _isRunning;
    
    private CancellationTokenSource? _cancellationTokenSource;

    public BatchResizeView()
    {
        InitializeComponent();
        Loaded += delegate
        {
            if (DataContext is not MainViewModel vm)
            {
                return;
            }

            SourceFolderTextBox.TextChanged += delegate { CheckIfValidDirectory(SourceFolderTextBox.Text); };
            SourceFolderTextBox.TextChanged += delegate
            {
                OutputFolderTextBox.Text = Path.Combine(SourceFolderTextBox.Text ?? string.Empty,
                    TranslationHelper.Translation.BatchResize);
            };

            SourceFolderButton.Click += async delegate
            {
                var directory = await FilePicker.SelectDirectory();
                if (string.IsNullOrWhiteSpace(directory))
                {
                    return;
                }

                SourceFolderTextBox.Text = directory;
            };

            OutputFolderButton.Click += async delegate
            {
                var directory = await FilePicker.SelectDirectory();
                if (string.IsNullOrWhiteSpace(directory))
                {
                    return;
                }

                OutputFolderTextBox.Text = directory;
            };

            LinkChainButton.Click += delegate { ToggleAspectRatio(); };

            StartButton.Click += async (_, _) =>
            {
                try
                {
                    await StartBatchResize();
                }
                catch (Exception e)
                {
#if DEBUG
                    Console.WriteLine(e);
#endif
                }
            };

            CancelButton.Click += async delegate
            {
                if (_isRunning)
                {
                    await CancelBatchResize();
                }
                else
                {
                    Reset();
                }

            };

            if (!NavigationHelper.CanNavigate(vm))
            {
                return;
            }

            SourceFolderTextBox.Text = vm.FileInfo?.DirectoryName ?? string.Empty;
        };
    }
    
    private void ToggleAspectRatio()
    {
        _isKeepingAspectRatio = !_isKeepingAspectRatio;
        LinkChainImage.IsVisible = _isKeepingAspectRatio;
        UnlinkChainImage.IsVisible = !_isKeepingAspectRatio;
    }

    private void Reset()
    {
        
    }
    
    private async Task CancelBatchResize()
    {
        await _cancellationTokenSource?.CancelAsync();
        CancelButtonTextBlock.Text = TranslationHelper.Translation.Reset;
        StartButton.IsEnabled = true;
        _isRunning = false;
        ProgressBar.Value = 0;
        await Task.CompletedTask;
    }

    private async Task StartBatchResize()
    {
        try
        {
            _cancellationTokenSource = new CancellationTokenSource();
            
            CancelButtonTextBlock.Text = TranslationHelper.Translation.Cancel;
            CancelButton.Classes.Remove("altHover");
            CancelButton.Classes.Add("errorHover");
            StartButton.IsEnabled = false;

            _isRunning = true;

            var files = await Task.FromResult(
                    FileListHelper.RetrieveFiles(new FileInfo(SourceFolderTextBox.Text)))
                .ConfigureAwait(true);

            if (!Directory.Exists(OutputFolderTextBox.Text))
            {
                Directory.CreateDirectory(OutputFolderTextBox.Text);
            }

            var outputFolder = string.IsNullOrWhiteSpace(OutputFolderTextBox.Text)
                ? SourceFolderTextBox.Text
                : OutputFolderTextBox.Text;

            var toConvert = !NoConversion.IsSelected;
            var pngSelected = PngItem.IsSelected;
            var jpgSelected = JpgItem.IsSelected;
            var webpSelected = WebpItem.IsSelected;
            var avifSelected = AvifItem.IsSelected;
            var heicSelected = HeicItem.IsSelected;
            var jxlSelected = JxlItem.IsSelected;

            var qualityEnabled = IsQualityEnabledBox.IsChecked.HasValue && IsQualityEnabledBox.IsChecked.Value;
            var qualityValue = (uint)QualitySlider.Value;

            var losslessCompress = Lossless.IsSelected;
            var lossyCompress = Lossy.IsSelected;

            Percentage? percentage = null;
            if (PercentageResizeBox.IsSelected)
            {
                if (int.TryParse(PercentageValueBox.Text, out var percentageValue))
                {
                    percentage = new Percentage(percentageValue);
                }
            }

            uint width = 0, height = 0;
            if (WidthResizeBox.IsSelected)
            {
                if (uint.TryParse(WidthValueBox.Text, out var widthValue))
                {
                    width = widthValue;
                }
            }
            else if (HeightResizeBox.IsSelected)
            {
                if (uint.TryParse(HeightValueBox.Text, out var heightValue))
                {
                    height = heightValue;
                }
            }
            else if (WidthAndHeightResizeBox.IsSelected)
            {
                if (uint.TryParse(WidthAndHeightWidthValueBox.Text, out var widthValue))
                {
                    width = widthValue;
                }

                if (uint.TryParse(WidthAndHeightHeightValueBox.Text, out var heightValue))
                {
                    height = heightValue;
                }
            }

            ProgressBar.Maximum = files.Count();
            ProgressBar.Value = 0;

            await Parallel.ForEachAsync(files, _cancellationTokenSource.Token, async (file, token) =>
            {
                token.ThrowIfCancellationRequested();
                
                var ext = Path.GetExtension(file);
                var destination = Path.Combine(outputFolder, Path.GetFileName(file));

                if (toConvert)
                {
                    string GetTargetExtension()
                    {
                        if (pngSelected) return ".png";
                        if (jpgSelected) return ".jpg";
                        if (webpSelected) return ".webp";
                        if (avifSelected) return ".avif";
                        if (heicSelected) return ".heic";
                        if (jxlSelected) return ".jxl";
                        return ext;
                    }

                    ext = GetTargetExtension();
                    destination = Path.ChangeExtension(destination, ext);
                }

                uint? quality = null;
                if (qualityEnabled)
                {
                    if (ext.Equals(".jpg", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(destination)
                            .Equals(".jpg", StringComparison.OrdinalIgnoreCase) ||
                        Path.GetExtension(destination).Equals(".jpeg", StringComparison.OrdinalIgnoreCase) ||
                        ext.Equals(".png", StringComparison.OrdinalIgnoreCase) || Path.GetExtension(destination)
                            .Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        quality = qualityValue;
                    }
                }

                var success = await SaveImageFileHelper.SaveImageAsync(null,
                    file,
                    destination,
                    width,
                    height,
                    quality,
                    ext,
                    null,
                    percentage,
                    losslessCompress,
                    lossyCompress,
                    _isKeepingAspectRatio).ConfigureAwait(false);

                if (success)
                {
                    await ProcessThumbs(file, Path.GetDirectoryName(destination), quality, ext).ConfigureAwait(false);

                    await Dispatcher.UIThread.InvokeAsync(() => { ProgressBar.Value++; });
                }
            }).ConfigureAwait(false);

            return;

            async Task ProcessThumbs(string? file, string? destinationDirectory, uint? quality, string? ext)
            {
                var toProcess = true;

                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    if (ThumbnailsComboBox.SelectedIndex <= 0)
                    {
                        toProcess = false;
                    }
                });

                if (!toProcess)
                {
                    return;
                }

                var destination = string.Empty;

                for (var i = 1; i <= 7; i++)
                {
                    bool thumbIsPercentageResized;
                    bool thumbIsWidthResized;
                    bool thumbIsHeightResized;

                    uint thumbWidth = 0, thumbHeight = 0;
                    Percentage? thumbPercentage = null;

                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        // Dynamically construct the control names
                        var percentageItemName = $"Thumb{i}PercentageItem";
                        var widthItemName = $"Thumb{i}WidthItem";
                        var heightItemName = $"Thumb{i}HeightItem";
                        var valueBoxName = $"Thumb{i}ValueBox";
                        var outputBoxName = $"Thumb{i}OutputBox";

                        // Find controls based on their names
                        var percentageItem = this.FindControl<ComboBoxItem>(percentageItemName);
                        var widthItem = this.FindControl<ComboBoxItem>(widthItemName);
                        var heightItem = this.FindControl<ComboBoxItem>(heightItemName);
                        var valueBox = this.FindControl<TextBox>(valueBoxName);
                        var outputBox = this.FindControl<TextBox>(outputBoxName);

                        // Check which resizing option is selected
                        thumbIsPercentageResized = percentageItem?.IsSelected ?? false;
                        thumbIsWidthResized = widthItem?.IsSelected ?? false;
                        thumbIsHeightResized = heightItem?.IsSelected ?? false;

                        // Parse the value from the TextBox
                        if (uint.TryParse(valueBox?.Text, out var thumbValue))
                        {
                            if (thumbIsPercentageResized)
                            {
                                thumbPercentage = new Percentage(thumbValue);
                            }

                            if (thumbIsWidthResized)
                            {
                                thumbWidth = thumbValue;
                            }

                            if (thumbIsHeightResized)
                            {
                                thumbHeight = thumbValue;
                            }
                        }

                        if (!Directory.Exists(destinationDirectory))
                        {
                            Directory.CreateDirectory(destinationDirectory);
                        }

                        destination = Path.Combine(destinationDirectory, outputBox.Text, Path.GetFileName(file));

                        if (!Directory.Exists(Path.GetDirectoryName(destination)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(destination));
                        }
                    });

                    var success = await SaveImageFileHelper.SaveImageAsync(null,
                        file,
                        destination,
                        thumbWidth,
                        thumbHeight,
                        quality,
                        ext,
                        null,
                        thumbPercentage,
                        losslessCompress,
                        lossyCompress,
                        _isKeepingAspectRatio).ConfigureAwait(false);

                    if (success)
                    {
                    }
                }
            }
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
        }
        finally
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ProgressBar.Value = 0;
                _isRunning = false;
                
                StartButton.IsEnabled = true;
                
                CancelButtonTextBlock.Text = TranslationHelper.Translation.Reset;
                CancelButton.Classes.Remove("errorHover");
                CancelButton.Classes.Add("altHover");
            });
            
        }
    }

    private void CheckIfValidDirectory(string path)
    {
        if (!Directory.Exists(path))
        {
            StartButton.IsEnabled = false;
            return;
        }

        StartButton.IsEnabled = true;
    }
}