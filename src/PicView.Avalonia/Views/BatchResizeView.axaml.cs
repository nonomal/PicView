using Avalonia.Controls;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;
using PicView.Core.Localization;

namespace PicView.Avalonia.Views;
public partial class BatchResizeView : UserControl
{

    private bool _isKeepingAspectRatio;
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
                OutputFolderTextBox.Text = Path.Combine(SourceFolderTextBox.Text ?? string.Empty, TranslationHelper.Translation.BatchResize);
            };

            LinkChainButton.Click += delegate
            {
                if (_isKeepingAspectRatio)
                {
                    _isKeepingAspectRatio = false;
                    LinkChainImage.IsVisible = true;
                    UnlinkChainImage.IsVisible = false;
                }
                else
                {
                    _isKeepingAspectRatio = true;
                    LinkChainImage.IsVisible = false;
                    UnlinkChainImage.IsVisible = true;
                }
            };
            
            if (!NavigationHelper.CanNavigate(vm))
            {
                return;
            }
            
            SourceFolderTextBox.Text = vm.FileInfo?.DirectoryName ?? string.Empty;
            
            StartButton.Click += async (_, _) => await StartBatchResize();
        };
    }

    private async Task StartBatchResize()
    {
        if (DataContext is not MainViewModel vm)
        {
            return;
        }
        var file = vm.FileInfo.FullName;
        var ext = vm.FileInfo.Extension;
        var destination = string.IsNullOrWhiteSpace(OutputFolderTextBox.Text) ? SourceFolderTextBox.Text : OutputFolderTextBox.Text;
        uint width = 0, height = 0;
        if (!NoConversion.IsSelected)
        {
            if (PngItem.IsSelected)
            {
                ext = ".png";
                destination = Path.ChangeExtension(destination, ".png");
            }
            else if (JpgItem.IsSelected)
            {
                ext = ".jpg";
                destination = Path.ChangeExtension(destination, ".jpg");
            }
            else if (WebpItem.IsSelected)
            {
                ext = ".webp";
                destination = Path.ChangeExtension(destination, ".webp");
            }
            else if (AvifItem.IsSelected)
            {
                ext = ".avif";
                destination = Path.ChangeExtension(destination, ".avif");
            }
            else if (HeicItem.IsSelected)
            {
                ext = ".heic";
                destination = Path.ChangeExtension(destination, ".heic");
            }
            else if (JxlItem.IsSelected)
            {
                ext = ".jxl";
                destination = Path.ChangeExtension(destination, ".jxl");
            }
        }

        uint? quality = null;
        if (QualitySlider.IsEnabled)
        {
            if (ext == ".jpg" || Path.GetExtension(destination) == ".jpg" || Path.GetExtension(destination) == ".jpeg")
            {
                quality = (uint)QualitySlider.Value;
            }
        }

        // var success = await SaveImageFileHelper.SaveImageAsync(null,
        //     file,
        //     destination,
        //     width,
        //     height,
        //     quality,
        //     ext,
        //     null,
        //     _isKeepingAspectRatio).ConfigureAwait(false);
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
