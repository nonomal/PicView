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
    
    private double _sketchStrokeWidth;
    private int _posterizeLevel;
    
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
            ResetPencilSketchBtn.Click += delegate
            {
                PencilSketchSlider.Value = 0;
            };
            ResetPencilSketchBtn.Click += delegate
            {
                PencilSketchSlider.Value = 0;
            };
            ResetPosterizeSketchBtn.Click += delegate
            {
                PosterizeSlider.Value = 0;
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
            
            PencilSketchSlider.ValueChanged += (_, e) =>
            {
                _sketchStrokeWidth = e.NewValue;
                DebounceSliderChange();
            };
            
            PosterizeSlider.ValueChanged += (_, e) =>
            {
                var newValue = (int)e.NewValue;
                _posterizeLevel = newValue is 1 ? 2 : newValue;
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

            OldMovieToggleButton.Click += async (_, _) =>
            {
                if (!OldMovieToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                if (OldMovieToggleButton.IsChecked.Value)
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
        bool negative = false, blackAndWhite = false, oldMovie = false;
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            negative = NegativeToggleButton.IsChecked.HasValue && NegativeToggleButton.IsChecked.Value;
            blackAndWhite = BlackAndWhiteToggleButton.IsChecked.HasValue && BlackAndWhiteToggleButton.IsChecked.Value;
            oldMovie = OldMovieToggleButton.IsChecked.HasValue && OldMovieToggleButton.IsChecked.Value;
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

        if (oldMovie)
        {
            // 1. Apply sepia tone
            magick.SepiaTone(new Percentage(80));

            // 2. Add noise
            magick.AddNoise(NoiseType.MultiplicativeGaussian);

            var random = new Random();
            
            // 3. Add vertical bands (simulate scratches)
            for (var i = 0; i < magick.Width; i += random.Next(1,50))
            {
                using var band = new MagickImage(new MagickColor("#3E382A"), (uint)random.Next(1,3), magick.Height);
                band.Evaluate(Channels.Alpha, EvaluateOperator.Set, 0.2); // semi-transparent
                magick.Composite(band, i, 0, CompositeOperator.Over);
            }
        }

        if (_sketchStrokeWidth is not 0)
        {
            magick.Charcoal(_sketchStrokeWidth, 0);
        }
        
        if (_posterizeLevel is not 0)
        {
            magick.Posterize(_posterizeLevel);
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