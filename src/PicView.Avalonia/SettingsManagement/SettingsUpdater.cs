using PicView.Avalonia.ViewModels;
using PicView.Core.Config;
using PicView.Core.Localization;

namespace PicView.Avalonia.SettingsManagement;
    public static class SettingsUpdater
    {
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
    }
