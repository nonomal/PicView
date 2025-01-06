using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Media.Imaging;
using ImageMagick;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;

namespace PicView.Avalonia.Views;

public partial class EffectsView : UserControl
{
    private Percentage _brightness = new(0);
    private Percentage _contrast = new(0);
    
    public EffectsView()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            BrightnessSlider.ValueChanged += async (_, e) => await BrightnessChanged(e).ConfigureAwait(false);
            ContrastSlider.ValueChanged += async (_, e) => await ContrastChanged(e).ConfigureAwait(false);
            EmbossSlider.ValueChanged += async (_, e) => await EmbossChanged(e).ConfigureAwait(false);
            if (DataContext is not MainViewModel vm)
            {
                return;
            }

            if (!NavigationHelper.CanNavigate(vm))
            {
                return;
            }

            // using var magick = new ImageMagick.MagickImage(vm.FileInfo);
            // BrightnessSlider.Value = magick.get

        };
    }

    private async Task BrightnessChanged(RangeBaseValueChangedEventArgs e)
    {
        _brightness = new Percentage(e.NewValue);
        if (DataContext is not MainViewModel vm)
        {
            return;
        }

        if (!NavigationHelper.CanNavigate(vm))
        {
            return;
        }
        await SetBrightnessContrast(vm);
    }
    
    private async Task ContrastChanged(RangeBaseValueChangedEventArgs e)
    {
        _contrast = new Percentage(e.NewValue);
        if (DataContext is not MainViewModel vm)
        {
            return;
        }

        if (!NavigationHelper.CanNavigate(vm))
        {
            return;
        }
        await SetBrightnessContrast(vm);
    }

    private async Task SetBrightnessContrast(MainViewModel vm)
    {
        using var magick = new MagickImage();
        await magick.ReadAsync(vm.FileInfo.FullName).ConfigureAwait(false);
        magick.BrightnessContrast(_brightness, _contrast);
        magick.Format = MagickFormat.WebP;
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        await using var memoryStream = new MemoryStream();
        await magick.WriteAsync(memoryStream);
        memoryStream.Position = 0;
        var bitmap = new Bitmap(memoryStream);
        vm.ImageSource = bitmap;
    }
    
    private async Task EmbossChanged(RangeBaseValueChangedEventArgs e)
    {
        if (DataContext is not MainViewModel vm)
        {
            return;
        }

        if (!NavigationHelper.CanNavigate(vm))
        {
            return;
        }
        using var magick = new MagickImage();
        await magick.ReadAsync(vm.FileInfo.FullName).ConfigureAwait(false);
        magick.Format = MagickFormat.WebP;
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        magick.Emboss(0, e.NewValue);
        await using var memoryStream = new MemoryStream();
        await magick.WriteAsync(memoryStream);
        memoryStream.Position = 0;
        var bitmap = new Bitmap(memoryStream);
        vm.ImageSource = bitmap;
    }
}