using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using ImageMagick;
using PicView.Avalonia.ImageHandling;
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
            ResetContrastBtn.Click += delegate
            {
                ContrastSlider.Value = 0;
            };
            ResetBrightnessBtn.Click += delegate
            {
                BrightnessSlider.Value = 0;
            };
            
            BrightnessSlider.ValueChanged += async (_, e) => await BrightnessChanged(e).ConfigureAwait(false);
            ContrastSlider.ValueChanged += async (_, e) => await ContrastChanged(e).ConfigureAwait(false);
            DirectionalBlurSlider.ValueChanged += async (_, e) => await DirectionalBlurChanged(e).ConfigureAwait(false);
            BlackAndWhiteToggleButton.Click += async (_, _) =>
            {
                if (!BlackAndWhiteToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                if (BlackAndWhiteToggleButton.IsChecked.Value)
                {
                    await ApplyEffects(DataContext as MainViewModel);
                }
                else
                {
                    await RemoveEffects(DataContext as MainViewModel);
                }
            };

            NegativeToggleButton.Click += async (_, _) =>
            {
                if (!NegativeToggleButton.IsChecked.HasValue)
                {
                    return;
                }
                if (NegativeToggleButton.IsChecked.Value)
                {
                    await ApplyEffects(DataContext as MainViewModel);
                }
                else
                {
                    await RemoveEffects(DataContext as MainViewModel);
                }
            };
            EmbossToggleButton.Click += async (_, _) =>
            {
                if (!EmbossToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                if (EmbossToggleButton.IsChecked.Value)
                {
                    await ApplyEffects(DataContext as MainViewModel);
                }
                else
                {
                    await RemoveEffects(DataContext as MainViewModel);
                }
            };
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
        await ApplyEffects(vm);
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
        await ApplyEffects(vm);
    }
    
    private async Task DirectionalBlurChanged(RangeBaseValueChangedEventArgs e)
    {
        if (DataContext is not MainViewModel vm)
        {
            return;
        }    

        if (!NavigationHelper.CanNavigate(vm))
        {
            return;
        }
        await ApplyEffects(vm);
    }

    private async Task ApplyEffects(MainViewModel vm)
    {
        var negative = NegativeToggleButton.IsChecked.HasValue && NegativeToggleButton.IsChecked.Value;
        var blackAndWhite = BlackAndWhiteToggleButton.IsChecked.HasValue && BlackAndWhiteToggleButton.IsChecked.Value;
        var emboss = EmbossToggleButton.IsChecked.HasValue && EmbossToggleButton.IsChecked.Value;
        using var magick = new MagickImage();
        await magick.ReadAsync(vm.FileInfo.FullName).ConfigureAwait(false);
        magick.BrightnessContrast(_brightness, _contrast);
        magick.Format = MagickFormat.WebP;
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        if (negative)
        {
            magick.Negate();
        }

        if (blackAndWhite)
        {
            magick.Grayscale();
        }

        if (emboss)
        {
            magick.Emboss();
        }
        var bitmap = magick.ToWriteableBitmap();
        vm.ImageSource = bitmap;
    }
    
    private static async Task RemoveEffects(MainViewModel vm)
    {
        using var magick = new MagickImage();
        await magick.ReadAsync(vm.FileInfo.FullName).ConfigureAwait(false);
        magick.Format = MagickFormat.WebP;
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        var bitmap = magick.ToWriteableBitmap();
        vm.ImageSource = bitmap;
    }
}