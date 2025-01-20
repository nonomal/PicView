using Avalonia.Controls;
using Avalonia.Threading;
using ImageMagick;
using PicView.Avalonia.ImageEffects;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;
using Timer = System.Timers.Timer;

namespace PicView.Avalonia.Views;

public partial class EffectsView : UserControl
{
    private CancellationTokenSource? _cancellationTokenSource;
    private ImageEffectConfig _config;

    private Timer? _debounceTimer;

    private bool _reloading;

    public EffectsView()
    {
        InitializeComponent();

        Loaded += (_, _) =>
        {
            if (DataContext is not MainViewModel vm)
            {
                return;
            }

            PointerPressed += (_, e) =>
            {
                if (e.GetCurrentPoint(this).Properties.IsRightButtonPressed)
                {
                    // Context menu doesn't want to be opened normally
                    ContextMenu.Open();
                }
            };

            DetachedFromLogicalTree += (_, _) => CleanUp();

            ClearEffectsItem.Click += async delegate { await RemoveEffects(vm); };
            ResetContrastBtn.Click += delegate { ContrastSlider.Value = 0; };
            ResetBrightnessBtn.Click += delegate { BrightnessSlider.Value = 0; };
            ResetPencilSketchBtn.Click += delegate { PencilSketchSlider.Value = 0; };
            ResetPosterizeBtn.Click += delegate { PosterizeSlider.Value = 0; };
            ResetSolarizeBtn.Click += delegate { SolarizeSlider.Value = 0; };
            ResetBlurBtn.Click += delegate { BlurSlider.Value = 0; };

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

            SolarizeSlider.ValueChanged += (_, e) =>
            {
                _config.Solarize = new Percentage(e.NewValue);
                DebounceSliderChange();
            };

            BlurSlider.ValueChanged += (_, e) =>
            {
                _config.BlurLevel = e.NewValue;
                DebounceSliderChange();
            };

            BlackAndWhiteToggleButton.Click += async (_, _) =>
            {
                if (!BlackAndWhiteToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                _config.BlackAndWhite = BlackAndWhiteToggleButton.IsChecked.Value;
                await ApplyEffectsDebounced(vm);
            };

            NegativeToggleButton.Click += async (_, _) =>
            {
                if (!NegativeToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                _config.Negative = NegativeToggleButton.IsChecked.Value;
                await ApplyEffectsDebounced(vm);
            };

            OldMovieToggleButton.Click += async (_, _) =>
            {
                if (!OldMovieToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                _config.OldMovie = OldMovieToggleButton.IsChecked.Value;
                await ApplyEffectsDebounced(vm);
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
        if (_reloading)
        {
            return;
        }
        if (_cancellationTokenSource is not null)
        {
            await _cancellationTokenSource.CancelAsync().ConfigureAwait(false);
        }

        _cancellationTokenSource = new CancellationTokenSource();
        await ImageEffectsHelper.ApplyEffects(vm, _config, _cancellationTokenSource.Token).ConfigureAwait(false);
    }

    private async Task RemoveEffects(MainViewModel vm)
    {
        _reloading = true;
        try
        {
            await ErrorHandling.ReloadImageAsync(vm).ConfigureAwait(false);
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                BlackAndWhiteToggleButton.IsChecked = false;
                NegativeToggleButton.IsChecked = false;
                OldMovieToggleButton.IsChecked = false;
                ContrastSlider.Value = 0;
                BrightnessSlider.Value = 0;
                PencilSketchSlider.Value = 0;
                PosterizeSlider.Value = 0;
                SolarizeSlider.Value = 0;
                BlurSlider.Value = 0;
            });
            _config = new ImageEffectConfig();
        }
        finally
        {
            _reloading = false;
        }
    }

    ~EffectsView()
    {
        CleanUp();
    }

    private void CleanUp()
    {
        if (DataContext is MainViewModel vm)
        {
            vm.IsLoading = false;
        }

        _debounceTimer?.Dispose();
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}