using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls.Primitives;
using Avalonia.Media;
using Avalonia.Threading;
using PicView.Avalonia.ColorManagement;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Config;
using PicView.Core.Gallery;
using PicView.Core.Localization;

namespace PicView.Avalonia.SettingsManagement;
public static class SettingsUpdater
{
    public static async Task ResetSettings(MainViewModel vm)
    {
        vm.IsLoading = true;

        try
        {
            SettingsHelper.DeleteSettingFiles();
            SettingsHelper.SetDefaults();

            ThemeManager.DetermineTheme(Application.Current, false);
        
            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                TurnOffUsingTouchpad(vm);
            }
            else
            {
                TurnOffUsingTouchpad(vm);
            }
        
            vm.GetBottomGalleryItemHeight = GalleryDefaults.DefaultBottomGalleryHeight;
            vm.GetFullGalleryItemHeight = GalleryDefaults.DefaultFullGalleryHeight;
        
            if (string.IsNullOrWhiteSpace(SettingsHelper.Settings.Gallery.BottomGalleryStretchMode))
            {
                SettingsHelper.Settings.Gallery.BottomGalleryStretchMode = "UniformToFill";
            }

            if (string.IsNullOrWhiteSpace(SettingsHelper.Settings.Gallery.FullGalleryStretchMode))
            {
                SettingsHelper.Settings.Gallery.FullGalleryStretchMode = "UniformToFill";
            }
        
            await TurnOffSubdirectories(vm);
        
            TurnOffSideBySide(vm);
            TurnOffScroll(vm);
            TurnOffCtrlZoom(vm);
            TurnOffLooping(vm);
        
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                vm.PlatformService.StopTaskbarProgress();
                if (NavigationHelper.CanNavigate(vm))
                {
                    vm.PlatformService.SetTaskbarProgress((ulong)vm.ImageIterator?.CurrentIndex!,
                        (ulong)vm.ImageIterator?.ImagePaths?.Count!);
                }
                WindowResizing.SetSize(vm);
            });
            
            await SettingsHelper.SaveSettingsAsync();
        }
        finally
        {
            SetTitleHelper.RefreshTitle(vm);
            vm.IsLoading = false;
        }
    }

    public static async Task ToggleUsingTouchpad(MainViewModel vm)
    {
        if (SettingsHelper.Settings.Zoom.IsUsingTouchPad)
        {
            TurnOffUsingTouchpad(vm);
        }
        else
        {
            TurnOnUsingTouchpad(vm);
        }
    
        await SettingsHelper.SaveSettingsAsync();
    }
    
    public static void TurnOffUsingTouchpad(MainViewModel vm)
    {
        SettingsHelper.Settings.Zoom.IsUsingTouchPad = false;
        vm.GetIsUsingTouchpadTranslation = TranslationHelper.Translation.UsingMouse;
        vm.IsUsingTouchpad = false;
    }
    
    public static void TurnOnUsingTouchpad(MainViewModel vm)
    {
        SettingsHelper.Settings.Zoom.IsUsingTouchPad = true;
        vm.GetIsUsingTouchpadTranslation = TranslationHelper.Translation.UsingTouchpad;
        vm.IsUsingTouchpad = true;
    }
    
    public static async Task ToggleSubdirectories(MainViewModel vm)
    {
        if (SettingsHelper.Settings.Sorting.IncludeSubDirectories)
        {
            await TurnOffSubdirectories(vm);
        }
        else
        {
            await TurnOnSubdirectories(vm);
        }
        await SettingsHelper.SaveSettingsAsync();
    }
    
    public static async Task TurnOffSubdirectories(MainViewModel vm)
    {
        vm.IsIncludingSubdirectories = false;
        SettingsHelper.Settings.Sorting.IncludeSubDirectories = false;
        
        await vm.ImageIterator.ReloadFileList();
        SetTitleHelper.SetTitle(vm);
    }
    
    public static async Task TurnOnSubdirectories(MainViewModel vm)
    {
        vm.IsIncludingSubdirectories = true;
        SettingsHelper.Settings.Sorting.IncludeSubDirectories = true;
        
        await vm.ImageIterator.ReloadFileList();
        SetTitleHelper.SetTitle(vm);
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
                    vm.PlatformService.SetTaskbarProgress((ulong)vm.ImageIterator?.CurrentIndex!,
                        (ulong)vm.ImageIterator?.ImagePaths?.Count!);
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
            TurnOffSideBySide(vm);
        }
        else
        {
            await TurnOnSideBySide(vm);
        }

        await SettingsHelper.SaveSettingsAsync();
    }

    public static void TurnOffSideBySide(MainViewModel vm)
    {
        SettingsHelper.Settings.ImageScaling.ShowImageSideBySide = false;
        vm.IsShowingSideBySide = false;
        vm.SecondaryImageSource = null;
        WindowResizing.SetSize(vm);
    }
    
    public static async Task TurnOnSideBySide(MainViewModel vm)
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
    
    public static async Task ToggleScroll(MainViewModel vm)
    {
        if (vm is null)
        {
            return;
        }
        
        if (SettingsHelper.Settings.Zoom.ScrollEnabled)
        {
            TurnOffScroll(vm);
        }
        else
        {
            TurnOnScroll(vm);
        }

        WindowResizing.SetSize(vm);
        
        await SettingsHelper.SaveSettingsAsync();
    }
    
    public static void TurnOffScroll(MainViewModel vm)
    {
        vm.ToggleScrollBarVisibility = ScrollBarVisibility.Disabled;
        vm.GetIsScrollingTranslation = TranslationHelper.Translation.ScrollingDisabled;
        vm.IsScrollingEnabled = false;
        SettingsHelper.Settings.Zoom.ScrollEnabled = false;
    }
    
    public static void TurnOnScroll(MainViewModel vm)
    {
        vm.ToggleScrollBarVisibility = ScrollBarVisibility.Visible;
        vm.GetIsScrollingTranslation = TranslationHelper.Translation.ScrollingEnabled;
        vm.IsScrollingEnabled = true;
        SettingsHelper.Settings.Zoom.ScrollEnabled = true;
    }
    
    public static async Task ToggleCtrlZoom(MainViewModel vm)
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
    
    public static void TurnOffCtrlZoom(MainViewModel vm)
    {
        SettingsHelper.Settings.Zoom.CtrlZoom = false;
        vm.GetIsCtrlZoomTranslation = TranslationHelper.Translation.ScrollToZoom;
        if (!Application.Current.TryGetResource("ScanEyeImage", Application.Current.RequestedThemeVariant, out var scanEyeImage ))
        {
            return;
        }
        vm.ChangeCtrlZoomImage = scanEyeImage as DrawingImage;
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
    
    public static void TurnOffLooping(MainViewModel vm)
    {
        SettingsHelper.Settings.UIProperties.Looping = false;
        vm.GetIsLoopingTranslation = TranslationHelper.Translation.LoopingDisabled;
        vm.IsLooping = false;
    }
    
    #endregion
}
