using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Config;
using PicView.Core.Localization;
using PicView.Core.ProcessHandling;

namespace PicView.Avalonia.SettingsManagement;
    public static class SettingsUpdater
    {
        public static async Task ResetSettings(MainViewModel vm)
        {
            SettingsHelper.DeleteSettingFiles();
            SettingsHelper.SetDefaults();
            await SettingsHelper.SaveSettingsAsync();
            string args;
            if (!NavigationHelper.CanNavigate(vm))
            {
                var argsList = Environment.GetCommandLineArgs();
                args = argsList.Length > 1 ? argsList[1] : string.Empty;
            }
            else
            {
                args = vm.FileInfo.FullName;
            }
            ProcessHelper.RestartApp(args);
            
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                desktop.MainWindow?.Close();
            });
        }
        
        public static async Task ToggleUsingTouchpad(MainViewModel vm)
        {
            SettingsHelper.Settings.Zoom.IsUsingTouchPad = !SettingsHelper.Settings
                .Zoom.IsUsingTouchPad;
        
            vm.GetIsUsingTouchpadTranslation = SettingsHelper.Settings
                .Zoom.IsUsingTouchPad
                ? TranslationHelper.Translation.UsingTouchpad
                : TranslationHelper.Translation.UsingMouse;
            
            vm.IsUsingTouchpad = SettingsHelper.Settings.Zoom.IsUsingTouchPad;
        
            await SettingsHelper.SaveSettingsAsync();
        }
        
        public static async Task ToggleSubdirectories(MainViewModel vm)
        {
            if (SettingsHelper.Settings.Sorting.IncludeSubDirectories)
            {
                vm.IsIncludingSubdirectories = false;
                SettingsHelper.Settings.Sorting.IncludeSubDirectories = false;
            }
            else
            {
                vm.IsIncludingSubdirectories = true;
                SettingsHelper.Settings.Sorting.IncludeSubDirectories = true;
            }

            await vm.ImageIterator.ReloadFileList();
            SetTitleHelper.SetTitle(vm);
            await SettingsHelper.SaveSettingsAsync();
        }
        
        public static async Task ToggleTaskbarProgress(MainViewModel vm)
        {
            if (SettingsHelper.Settings.UIProperties.IsTaskbarProgressEnabled)
            {
                SettingsHelper.Settings.UIProperties.IsTaskbarProgressEnabled = false;
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    vm.PlatformService.StopTaskbarProgress();
                });
            }
            else
            {
                SettingsHelper.Settings.UIProperties.IsTaskbarProgressEnabled = true;
                if (NavigationHelper.CanNavigate(vm))
                {
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        vm.PlatformService.SetTaskbarProgress((ulong)vm.ImageIterator.CurrentIndex,
                            (ulong)vm.ImageIterator.ImagePaths.Count);
                    });
                }
            }

            await SettingsHelper.SaveSettingsAsync();
        }
        
        #region Image settings

        public static async Task ToggleSideBySide(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            
            if (SettingsHelper.Settings.ImageScaling.ShowImageSideBySide)
            {
                SettingsHelper.Settings.ImageScaling.ShowImageSideBySide = false;
                vm.IsShowingSideBySide = false;
                vm.SecondaryImageSource = null;
                WindowResizing.SetSize(vm);
            }
            else
            {
                SettingsHelper.Settings.ImageScaling.ShowImageSideBySide = true;
                vm.IsShowingSideBySide = true;
                if (NavigationHelper.CanNavigate(vm))
                {
                    var preloadValue = await vm.ImageIterator?.GetNextPreLoadValueAsync();
                    vm.SecondaryImageSource = preloadValue?.ImageModel.Image;
                    await Dispatcher.UIThread.InvokeAsync(() =>
                    {
                        WindowResizing.SetSize(vm.ImageWidth, vm.ImageHeight, preloadValue.ImageModel.PixelWidth,
                            preloadValue.ImageModel.PixelHeight, vm.RotationAngle, vm);
                    });
                }
            }

            await SettingsHelper.SaveSettingsAsync();
        }
        
        public static async Task ToggleScroll(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            
            if (SettingsHelper.Settings.Zoom.ScrollEnabled)
            {
                vm.ToggleScrollBarVisibility = ScrollBarVisibility.Disabled;
                vm.GetIsScrollingTranslation = TranslationHelper.Translation.ScrollingDisabled;
                vm.IsScrollingEnabled = false;
                SettingsHelper.Settings.Zoom.ScrollEnabled = false;
            }
            else
            {
                vm.ToggleScrollBarVisibility = ScrollBarVisibility.Visible;
                vm.GetIsScrollingTranslation = TranslationHelper.Translation.ScrollingEnabled;
                vm.IsScrollingEnabled = true;
                SettingsHelper.Settings.Zoom.ScrollEnabled = true;
            }

            WindowResizing.SetSize(vm);
            
            await SettingsHelper.SaveSettingsAsync();
        }
        
        public static async Task ChangeCtrlZoom(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            
            SettingsHelper.Settings.Zoom.CtrlZoom = !SettingsHelper.Settings.Zoom.CtrlZoom;
            vm.GetIsCtrlZoomTranslation = SettingsHelper.Settings.Zoom.CtrlZoom
                ? TranslationHelper.Translation.CtrlToZoom
                : TranslationHelper.Translation.ScrollToZoom;
            
            // Set source for ChangeCtrlZoomImage
            if (!Application.Current.TryGetResource("ScanEyeImage", Application.Current.RequestedThemeVariant, out var scanEyeImage ))
            {
                return;
            }
            if (!Application.Current.TryGetResource("LeftRightArrowsImage", Application.Current.RequestedThemeVariant, out var leftRightArrowsImage ))
            {
                return;
            }
            var isNavigatingWithCtrl = SettingsHelper.Settings.Zoom.CtrlZoom;
            vm.ChangeCtrlZoomImage = isNavigatingWithCtrl ? leftRightArrowsImage as DrawingImage : scanEyeImage as DrawingImage;
            await SettingsHelper.SaveSettingsAsync().ConfigureAwait(false);
        }
        
        public static async Task ToggleLooping(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            
            var value = !SettingsHelper.Settings.UIProperties.Looping;
            SettingsHelper.Settings.UIProperties.Looping = value;
            vm.GetIsLoopingTranslation = value
                ? TranslationHelper.Translation.LoopingEnabled
                : TranslationHelper.Translation.LoopingDisabled;
            vm.IsLooping = value;

            var msg = value
                ? TranslationHelper.Translation.LoopingEnabled
                : TranslationHelper.Translation.LoopingDisabled;
            await TooltipHelper.ShowTooltipMessageAsync(msg);

            await SettingsHelper.SaveSettingsAsync();
        }
        
        #endregion
    }
