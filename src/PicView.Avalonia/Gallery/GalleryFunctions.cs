using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Layout;
using PicView.Avalonia.Helpers;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.ViewModels;
using PicView.Core.Config;
using PicView.Core.FileHandling;
using PicView.Core.Gallery;

namespace PicView.Avalonia.Gallery
{
    public static class GalleryFunctions
    {
        public static bool isFullGalleryOpen { get; private set; }
        public static bool isBottomGalleryOpen { get; private set; }
        public static bool isAnyGalleryOpen => isFullGalleryOpen || isBottomGalleryOpen;

        public static void RecycleItem(object sender, MainViewModel vm)
        {
#if DEBUG
            Debug.Assert(sender != null, nameof(sender) + " != null");
#endif
            var menuItem = (MenuItem)sender;
            // ReSharper disable once ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
            if (menuItem is null) { return; }
#if DEBUG
            Debug.Assert(menuItem != null, nameof(menuItem) + " != null");
            Debug.Assert(menuItem.DataContext != null, "menuItem.DataContext != null");
#endif
            var galleryItem = (GalleryThumbInfo.GalleryThumbHolder)menuItem.DataContext;
            FileDeletionHelper.DeleteFileWithErrorMsg(galleryItem.FileLocation, recycle: true);

            vm.GalleryItems.Remove(galleryItem); // TODO: rewrite file system watcher to delete gallery items
        }

        public static async Task ToggleGallery(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }

            if (SettingsHelper.Settings.Gallery.IsBottomGalleryShown)
            {
                if (isFullGalleryOpen)
                {
                    OpenBottomGallery(vm);
                }
                else
                {
                    OpenFullGallery(vm);
                }
                vm.CloseMenuCommand.Execute(null);
                SetGalleryItemSize(vm);
                return;
            }

            OpenFullGallery(vm);
            vm.CloseMenuCommand.Execute(null);
            if (isAnyGalleryOpen)
            {
                if (!NavigationHelper.CanNavigate(vm))
                {
                    return;
                }
                _ = Task.Run(() => GalleryLoad.LoadGallery(vm, Path.GetDirectoryName(vm.ImageIterator.Pics[0])));
            }

            await SettingsHelper.SaveSettingsAsync();
        }

        public static async Task ToggleBottomGallery(MainViewModel vm)
        {
            if (vm is null)
            {
                return;
            }
            if (SettingsHelper.Settings.Gallery.IsBottomGalleryShown)
            {
                OpenBottomGallery(vm);
                SetGalleryItemSize(vm);
            }
            else
            {
                SettingsHelper.Settings.Gallery.IsBottomGalleryShown = true;
                if (!NavigationHelper.CanNavigate(vm))
                {
                    return;
                }
                _ = Task.Run(() => GalleryLoad.LoadGallery(vm, Path.GetDirectoryName(vm.ImageIterator.Pics[0])));
            }
            vm.CloseMenuCommand.Execute(null);

            await SettingsHelper.SaveSettingsAsync();
        }

        public static async Task OpenCloseBottomGallery(MainViewModel vm)
        {
            if (SettingsHelper.Settings.Gallery.IsBottomGalleryShown)
            {
                CloseGallery(vm);
                SettingsHelper.Settings.Gallery.IsBottomGalleryShown = false;
                await SettingsHelper.SaveSettingsAsync();
                return;
            }

            OpenBottomGallery(vm);
            SettingsHelper.Settings.Gallery.IsBottomGalleryShown = true;
            await SettingsHelper.SaveSettingsAsync();
            if (!NavigationHelper.CanNavigate(vm))
            {
                return;
            }
            vm.CloseMenuCommand.Execute(null);
            await Task.Run(() => GalleryLoad.LoadGallery(vm, Path.GetDirectoryName(vm.ImageIterator.Pics[0])));
        }

        public static void OpenBottomGallery(MainViewModel vm)
        {
            vm.GalleryVerticalAlignment = VerticalAlignment.Bottom;
            vm.GalleryOrientation = Orientation.Horizontal;
            vm.IsGalleryCloseIconVisible = false;
            isBottomGalleryOpen = true;
            isFullGalleryOpen = false;
            vm.IsGalleryOpen = true;
        }

        public static void OpenFullGallery(MainViewModel vm)
        {
            vm.GalleryVerticalAlignment = VerticalAlignment.Stretch;
            vm.GalleryOrientation = Orientation.Vertical;
            vm.IsGalleryCloseIconVisible = true;
            isBottomGalleryOpen = false;
            isFullGalleryOpen = true;
            vm.IsGalleryOpen = true;
        }

        public static void CloseGallery(MainViewModel vm)
        {
            isBottomGalleryOpen = false;
            isFullGalleryOpen = false;
            vm.IsGalleryOpen = false;
        }

        public static void SetGalleryItemSize(MainViewModel vm)
        {
            if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }

            var screen = ScreenHelper.GetScreen(desktop.MainWindow);
            var size = isBottomGalleryOpen ? SettingsHelper.Settings.Gallery.BottomGalleryItemSize :
                SettingsHelper.Settings.Gallery.ExpandedGalleryItemSize;
            vm.GalleryItemSize = screen.WorkingArea.Height / size;
        }
    }
}