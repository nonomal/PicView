using System.Reactive.Disposables;
using System.Reactive.Linq;
using Avalonia.Controls;
using Avalonia.Threading;
using ImageMagick;
using PicView.Avalonia.ImageEffects;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;
using ReactiveUI;
using Timer = System.Timers.Timer;

namespace PicView.Avalonia.Views;

public partial class EffectsView : UserControl
{
    private CancellationTokenSource? _cancellationTokenSource;

    private Timer? _debounceTimer;

    private bool _reloading;
    private readonly CompositeDisposable _disposables = new();

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
            
            vm.ObservableForProperty(v => v.FileInfo)
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(_ => Reset())
                .DisposeWith(_disposables);

            if (vm.EffectConfig is null)
            {
                vm.EffectConfig = new ImageEffectConfig();
            }
            else
            {
                if (vm.EffectConfig.BlackAndWhite)
                {
                    BlackAndWhiteToggleButton.IsChecked = true;
                }

                if (vm.EffectConfig.OldMovie)
                {
                    OldMovieToggleButton.IsChecked = true;
                }

                if (vm.EffectConfig.Negative)
                {
                    NegativeToggleButton.IsChecked = true;
                }

                BrightnessSlider.Value = vm.EffectConfig.Brightness.ToInt32();
                ContrastSlider.Value = vm.EffectConfig.Contrast.ToInt32();
                PencilSketchSlider.Value = vm.EffectConfig.SketchStrokeWidth;
                PosterizeSlider.Value = vm.EffectConfig.PosterizeLevel;
                SolarizeSlider.Value = vm.EffectConfig.Solarize.ToInt32();
                BlurSlider.Value = vm.EffectConfig.BlurLevel;
            }

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
                vm.EffectConfig.Brightness = new Percentage(e.NewValue);
                DebounceSliderChange();
            };

            ContrastSlider.ValueChanged += (_, e) =>
            {
                vm.EffectConfig.Contrast = new Percentage(e.NewValue);
                DebounceSliderChange();
            };

            PencilSketchSlider.ValueChanged += (_, e) =>
            {
                vm.EffectConfig.SketchStrokeWidth = e.NewValue;
                DebounceSliderChange();
            };

            PosterizeSlider.ValueChanged += (_, e) =>
            {
                var newValue = (int)e.NewValue;
                vm.EffectConfig.PosterizeLevel = newValue is 1 ? 2 : newValue;
                DebounceSliderChange();
            };

            SolarizeSlider.ValueChanged += (_, e) =>
            {
                vm.EffectConfig.Solarize = new Percentage(e.NewValue);
                DebounceSliderChange();
            };

            BlurSlider.ValueChanged += (_, e) =>
            {
                vm.EffectConfig.BlurLevel = e.NewValue;
                DebounceSliderChange();
            };

            BlackAndWhiteToggleButton.Click += async (_, _) =>
            {
                if (!BlackAndWhiteToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                vm.EffectConfig.BlackAndWhite = BlackAndWhiteToggleButton.IsChecked.Value;
                await ApplyEffectsDebounced(vm);
            };

            NegativeToggleButton.Click += async (_, _) =>
            {
                if (!NegativeToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                vm.EffectConfig.Negative = NegativeToggleButton.IsChecked.Value;
                await ApplyEffectsDebounced(vm);
            };

            OldMovieToggleButton.Click += async (_, _) =>
            {
                if (!OldMovieToggleButton.IsChecked.HasValue)
                {
                    return;
                }

                vm.EffectConfig.OldMovie = OldMovieToggleButton.IsChecked.Value;
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
        await ImageEffectsHelper.ApplyEffects(vm, vm.EffectConfig, _cancellationTokenSource.Token).ConfigureAwait(false);
    }

    private async Task RemoveEffects(MainViewModel vm)
    {
        _reloading = true;
        try
        {
            await ErrorHandling.ReloadImageAsync(vm).ConfigureAwait(false);
            await Dispatcher.UIThread.InvokeAsync(Reset);
            vm.EffectConfig = new ImageEffectConfig();
        }
        finally
        {
            _reloading = false;
        }
    }

    private void Reset()
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
        if (DataContext is MainViewModel vm)
        {
            vm.EffectConfig = new ImageEffectConfig();
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

        _disposables.Dispose();
        _debounceTimer?.Dispose();
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource?.Dispose();
    }
}