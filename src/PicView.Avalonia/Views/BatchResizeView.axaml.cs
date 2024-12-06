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

            LinkChainButton.Click += delegate
            {
                if (_isKeepingAspectRatio)
                {
                    _isKeepingAspectRatio = false;
                    LinkChainImage.IsVisible = false;
                    UnlinkChainImage.IsVisible = true;
                }
                else
                {
                    _isKeepingAspectRatio = true;
                    LinkChainImage.IsVisible = true;
                    UnlinkChainImage.IsVisible = false;
                }
            };

            StartButton.Click += async (_, _) => await StartBatchResize();

            if (!NavigationHelper.CanNavigate(vm))
            {
                return;
            }

            SourceFolderTextBox.Text = vm.FileInfo?.DirectoryName ?? string.Empty;
        };
    }

    private async Task StartBatchResize()
    {
        if (DataContext is not MainViewModel vm)
        {
            return;
        }

        var files = await Task.FromResult(FileListHelper.RetrieveFiles(new FileInfo(SourceFolderTextBox.Text)));

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

        await Parallel.ForEachAsync(files, async (file, token) =>
        {
            var ext = Path.GetExtension(file);

            var destination = Path.Combine(outputFolder, Path.GetFileName(file));

            if (toConvert)
            {
                if (pngSelected)
                {
                    ext = ".png";
                    destination = Path.ChangeExtension(destination, ".png");
                }
                else if (jpgSelected)
                {
                    ext = ".jpg";
                    destination = Path.ChangeExtension(destination, ".jpg");
                }
                else if (webpSelected)
                {
                    ext = ".webp";
                    destination = Path.ChangeExtension(destination, ".webp");
                }
                else if (avifSelected)
                {
                    ext = ".avif";
                    destination = Path.ChangeExtension(destination, ".avif");
                }
                else if (heicSelected)
                {
                    ext = ".heic";
                    destination = Path.ChangeExtension(destination, ".heic");
                }
                else if (jxlSelected)
                {
                    ext = ".jxl";
                    destination = Path.ChangeExtension(destination, ".jxl");
                }
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
#if DEBUG
                Console.WriteLine($"Saved {file} to {destination}");
#endif
                await Dispatcher.UIThread.InvokeAsync(() => { ProgressBar.Value++; });
            }
        });
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