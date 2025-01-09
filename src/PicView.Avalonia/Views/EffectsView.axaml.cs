using Avalonia.Controls;
using Avalonia.Threading;
using ImageMagick;
using PicView.Avalonia.ImageHandling;
using PicView.Avalonia.ViewModels;
using Timer = System.Timers.Timer;

namespace PicView.Avalonia.Views;

public partial class EffectsView : UserControl
{
    private Percentage _brightness = new(0);
    private Percentage _contrast = new(0);
    
    private Timer? _debounceTimer;
    
    public EffectsView()
    {
        InitializeComponent();
        // if (Application.Current.TryGetResource("AccentColor",
        //         Application.Current.RequestedThemeVariant, out var accentColor))
        // {
        //     if (accentColor is SolidColorBrush accentBrush)
        //     {
        //         var brush = new SolidColorBrush
        //         {
        //             Opacity = 0.3,
        //             Color = new Color(accentBrush.Color.A, accentBrush.Color.R, accentBrush.Color.G,
        //                 accentBrush.Color.B)
        //         };
        //         brush.Opacity = 0.3;
        //         PlaceholderSlider.Background = brush;
        //     }
        // }
        
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
            ResetPlaceholderBtn.Click += delegate
            {
                PlaceholderSlider.Value = 0;
            };

            if (DataContext is not MainViewModel vm)
            {
                return;
            }
            
            _debounceTimer = new Timer { Interval = 300, AutoReset = false };
            _debounceTimer.Elapsed += async (_, _) => await ApplyEffectsDebounced(vm).ConfigureAwait(false);

            BrightnessSlider.ValueChanged += (_, e) =>
            {
                _brightness = new Percentage(e.NewValue);
                DebounceSliderChange();
            };
            
            ContrastSlider.ValueChanged += (_, e) =>
            {
                _contrast = new Percentage(e.NewValue);
                DebounceSliderChange();
            };
            
            PlaceholderSlider.ValueChanged += (_, e) =>
            {
                //
                DebounceSliderChange();
            };
            
            BlackAndWhiteToggleButton.Click += async (_, _) =>
            {
                if (!BlackAndWhiteToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                if (BlackAndWhiteToggleButton.IsChecked.Value)
                {
                    await ApplyEffects(vm);
                }
                else
                {
                    await RemoveEffects(vm);
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
                    await ApplyEffects(vm);
                }
                else
                {
                    await RemoveEffects(vm);
                }
            };

            PencilSketchToggleButton.Click += async (_, _) =>
            {
                if (!PencilSketchToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                if (PencilSketchToggleButton.IsChecked.Value)
                {
                    await ApplyEffects(vm);
                }
                else
                {
                    await RemoveEffects(vm);
                }
            };
        };
    }
    
    private void DebounceSliderChange()
    {
        _debounceTimer.Stop();
        _debounceTimer.Start();
    }

    private async Task ApplyEffectsDebounced(MainViewModel vm)
    {
        // Your image processing logic here
        await ApplyEffects(vm);
    }

    private async Task ApplyEffects(MainViewModel vm)
    {
        var fileInfo = vm.FileInfo;
        bool negative = false, blackAndWhite = false, pencilSketch = false;
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            negative = NegativeToggleButton.IsChecked.HasValue && NegativeToggleButton.IsChecked.Value;
            blackAndWhite = BlackAndWhiteToggleButton.IsChecked.HasValue && BlackAndWhiteToggleButton.IsChecked.Value;
            pencilSketch = PencilSketchToggleButton.IsChecked.HasValue && PencilSketchToggleButton.IsChecked.Value;
        });

        using var magick = new MagickImage();
        if (fileInfo.Length >= 2147483648)
        {
            // Fixes "The file is too long. This operation is currently limited to supporting files less than 2 gigabytes in size."
            // ReSharper disable once MethodHasAsyncOverload
            magick.Read(fileInfo);
        }
        else
        {
            await magick.ReadAsync(fileInfo).ConfigureAwait(false);
        }
        magick.BrightnessContrast(_brightness, _contrast);
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

        if (pencilSketch)
        {
            magick.Charcoal();
        }
        
        var bitmap = magick.ToWriteableBitmap();
        vm.ImageSource = bitmap;
    }
    
    private static async Task RemoveEffects(MainViewModel vm)
    {
        using var magick = new MagickImage();
        await magick.ReadAsync(vm.FileInfo.FullName).ConfigureAwait(false);
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        var bitmap = magick.ToWriteableBitmap();
        vm.ImageSource = bitmap;
    }
}