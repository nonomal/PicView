using Avalonia.Controls;
using ImageMagick;
using PicView.Avalonia.ImageEffects;
using PicView.Avalonia.ImageHandling;
using PicView.Avalonia.ViewModels;
using Timer = System.Timers.Timer;

namespace PicView.Avalonia.Views;

public partial class EffectsView : UserControl
{
    private ImageEffectConfig _config;
    
    private Timer? _debounceTimer;
    
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
                _config.Brightness = new Percentage(e.NewValue);
                DebounceSliderChange();
            };
            
            ContrastSlider.ValueChanged += (_, e) =>
            {
                _config.Contrast = new Percentage(e.NewValue);
                DebounceSliderChange();
            };
            
            PencilSketchSlider.ValueChanged += (_, e) =>
            {
                _config.SketchStrokeWidth = e.NewValue;
                DebounceSliderChange();
            };
            
            PosterizeSlider.ValueChanged += (_, e) =>
            {
                var newValue = (int)e.NewValue;
                _config.PosterizeLevel = newValue is 1 ? 2 : newValue;
                DebounceSliderChange();
            };
            
            BlackAndWhiteToggleButton.Click += async (_, _) =>
            {
                if (!BlackAndWhiteToggleButton.IsChecked.HasValue)
                {
                    return;
                }
                
                _config.BlackAndWhite = BlackAndWhiteToggleButton.IsChecked.Value;
                await ApplyEffects(vm);
            };

            NegativeToggleButton.Click += async (_, _) =>
            {
                if (!NegativeToggleButton.IsChecked.HasValue)
                {
                    return;

                }

                _config.Negative = NegativeToggleButton.IsChecked.Value;
                await ApplyEffects(vm);
            };

            OldMovieToggleButton.Click += async (_, _) =>
            {
                if (!OldMovieToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                _config.OldMovie = OldMovieToggleButton.IsChecked.Value;
                await ApplyEffects(vm);
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
        magick.BrightnessContrast(_config.Brightness, _config.Contrast);
        magick.BackgroundColor = MagickColors.Transparent;
        magick.Settings.BackgroundColor = MagickColors.Transparent;
        magick.Settings.FillColor = MagickColors.Transparent;
        if (_config.Negative)
        {
            magick.Negate();
        }

        if (_config.BlackAndWhite)
        {
            magick.Grayscale();
        }

        if (_config.OldMovie)
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

        if (_config.SketchStrokeWidth is not 0)
        {
            magick.Charcoal(_config.SketchStrokeWidth, 0);
        }
        
        if (_config.PosterizeLevel is not 0)
        {
            magick.Posterize(_config.PosterizeLevel);
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