using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Threading;
using PicView.Avalonia.Clipboard;
using PicView.Avalonia.Gallery;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Core.Calculations;
using PicView.Core.FileHandling;
using PicView.Core.Gallery;
using StartUpMenu = PicView.Avalonia.Views.StartUpMenu;

namespace PicView.Avalonia.Navigation;

public static class ErrorHandling
{
    public static void ShowStartUpMenu(MainViewModel vm)
    {
        if (vm is null)
        {
            return;
        }

        if (Dispatcher.UIThread.CheckAccess())
        {
            Start();
        }
        else
        {
            Dispatcher.UIThread.Post(Start);
        }

        return;
        void Start()
        {
            if (vm.CurrentView is not StartUpMenu)
            {
                var startUpMenu = new StartUpMenu();
                if (Settings.WindowProperties.AutoFit)
                {
                    startUpMenu.Width = SizeDefaults.WindowMinSize;
                    startUpMenu.Height = SizeDefaults.WindowMinSize;
                    if (Settings.Gallery.IsBottomGalleryShown)
                    {
                        vm.GalleryWidth = SizeDefaults.WindowMinSize;
                    }
                }
                vm.CurrentView = startUpMenu;
            }
            else
            {
                SetTitleHelper.ResetTitle(vm);
            }

            vm.GalleryMode = GalleryMode.Closed;
            GalleryFunctions.Clear();
            UIHelper.CloseMenus(vm);
            vm.ImageIterator?.Dispose();
            vm.ImageIterator = null;
            vm.GalleryMargin = new Thickness(0, 0, 0, 0);
            vm.GetIndex = 0;
            vm.PlatformService.StopTaskbarProgress();
            vm.IsLoading = false;
        }
    }

    public static async Task ReloadAsync(MainViewModel vm)
    {
        vm.IsLoading = true;
        
        if (vm.ImageSource is null)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ShowStartUpMenu(vm);
            });
            return;
        }
        
        if (!NavigationManager.CanNavigate(vm))
        {
            await FileHistoryNavigation.OpenLastFileAsync(vm);
            return;
        }
        
        if (vm.ImageIterator is null)
        {
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                ShowStartUpMenu(vm);
            });
            return;
        }

        try
        {
            var index = vm.ImageIterator.CurrentIndex;
            await vm.ImageIterator.DisposeAsync().ConfigureAwait(false);
            vm.ImageIterator = new ImageIterator(vm.FileInfo, vm.ImageIterator.ImagePaths, index, vm);
            await NavigationManager.Navigate(index, vm).ConfigureAwait(false);
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            await Dispatcher.UIThread.InvokeAsync(() => { ShowStartUpMenu(vm); });
        }
        finally
        {
            vm.IsLoading = false;
        }
    }
    
    public static async Task ReloadImageAsync(MainViewModel vm)
    {
        if (vm.ImageSource is null)
        {
            return;
        }

        if (NavigationManager.CanNavigate(vm))
        {
            var preloadValue = await vm.ImageIterator.GetPreLoadValueAsync(vm.ImageIterator.CurrentIndex).ConfigureAwait(false);
            if (preloadValue?.ImageModel.Image is not null)
            {
                vm.ImageSource = preloadValue.ImageModel.Image;
            }
        }
        else
        {
            var url = vm.Title.GetURL();
            if (!string.IsNullOrEmpty(url))
            {
                await NavigationManager.LoadPicFromUrlAsync(url, vm).ConfigureAwait(false);
            }
            else 
            {
                if (Application.Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
                {
                    return;
                }
                var clipboard = desktop.MainWindow.Clipboard;
                await ClipboardHelper.PasteClipboardImage(vm, clipboard);
            }
        }
    }
}
