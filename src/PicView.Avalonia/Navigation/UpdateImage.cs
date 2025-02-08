using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Threading;
using ImageMagick;
using PicView.Avalonia.Gallery;
using PicView.Avalonia.ImageHandling;
using PicView.Avalonia.Preloading;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Gallery;
using PicView.Core.ImageDecoding;
using PicView.Core.Navigation;

namespace PicView.Avalonia.Navigation;

public static class UpdateImage
{
    #region Update Source and Preview

    public static async Task UpdateSource(MainViewModel vm, int index, List<string> imagePaths, bool isReversed,
        PreLoadValue? preLoadValue,
        PreLoadValue? nextPreloadValue = null)
    {
        preLoadValue ??= await vm.ImageIterator.GetPreLoadValueAsync(index).ConfigureAwait(false);
        if (preLoadValue.ImageModel?.Image is null && index == vm.ImageIterator.CurrentIndex)
        {
            var fileInfo = preLoadValue.ImageModel?.FileInfo ?? new FileInfo(imagePaths[index]);
            preLoadValue.ImageModel = await GetImageModel.GetImageModelAsync(fileInfo).ConfigureAwait(false);
        }

        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            nextPreloadValue ??= await vm.ImageIterator.GetNextPreLoadValueAsync().ConfigureAwait(false);
            if (nextPreloadValue.ImageModel?.Image is null && index == vm.ImageIterator.CurrentIndex)
            {
                var fileInfo = nextPreloadValue.ImageModel?.FileInfo ?? new FileInfo(
                    imagePaths[
                        vm.ImageIterator.GetIteration(index, isReversed ? NavigateTo.Previous : NavigateTo.Next,
                            true)]);
                nextPreloadValue.ImageModel = await GetImageModel.GetImageModelAsync(fileInfo).ConfigureAwait(false);
            }
        }
        
        if (index != vm.ImageIterator.CurrentIndex)
        {
            return;
        }

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            if (index != vm.ImageIterator.CurrentIndex)
            {
                return;
            }
            vm.ImageViewer.SetTransform(preLoadValue.ImageModel.EXIFOrientation);
            if (Settings.ImageScaling.ShowImageSideBySide)
            {
                vm.SecondaryImageSource = nextPreloadValue.ImageModel.Image;
            }

            vm.ImageSource = preLoadValue.ImageModel.Image;
            if (preLoadValue.ImageModel.ImageType is ImageType.AnimatedGif or ImageType.AnimatedWebp)
            {
                vm.ImageViewer.MainImage.InitialAnimatedSource = preLoadValue.ImageModel.FileInfo.FullName;
            }

            vm.ImageType = preLoadValue.ImageModel.ImageType;

            WindowResizing.SetSize(preLoadValue.ImageModel.PixelWidth, preLoadValue.ImageModel.PixelHeight,
                nextPreloadValue?.ImageModel?.PixelWidth ?? 0, nextPreloadValue?.ImageModel?.PixelHeight ?? 0,
                preLoadValue.ImageModel.Rotation, vm);
            
            UIHelper.GetToolTipMessage.IsVisible = false;
        }, DispatcherPriority.Send);

        vm.IsLoading = false;

        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            SetTitleHelper.SetSideBySideTitle(vm, preLoadValue.ImageModel, nextPreloadValue?.ImageModel);
        }
        else
        {
            SetTitleHelper.SetTitle(vm, preLoadValue.ImageModel);
        }
        
        if (Settings.WindowProperties.KeepCentered)
        {
            await Dispatcher.UIThread.InvokeAsync(() => { WindowFunctions.CenterWindowOnScreen(); });
        }
        
        if (vm.SelectedGalleryItemIndex != index)
        {
            vm.SelectedGalleryItemIndex = index;
            if (Settings.Gallery.IsBottomGalleryShown)
            {
                GalleryNavigation.CenterScrollToSelectedItem(vm);
            }
        }

        vm.PixelWidth = preLoadValue.ImageModel.PixelWidth;
        vm.PixelHeight = preLoadValue.ImageModel.PixelHeight;
        vm.GetIndex = index + 1;
        vm.ExifOrientation = preLoadValue.ImageModel.EXIFOrientation;
        vm.FileInfo = preLoadValue.ImageModel.FileInfo;
        vm.ZoomValue = 1;
        
        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            // Fixes incorrect rendering in the side by side view
            // TODO: Improve and fix side by side and remove this hack 
            Dispatcher.UIThread.Post(() =>
            {
                vm.ImageViewer?.MainImage?.InvalidateVisual();
            });
        }

        // Reset effects
        vm.EffectConfig = null;
    }

    /// <summary>
    /// Sets the given image as the single image displayed in the view.
    /// </summary>
    /// <param name="source">The source of the image to display.</param>
    /// <param name="imageType"></param>
    /// <param name="name">The name of the image.</param>
    /// <param name="vm">The main view model instance.</param>
    public static void SetSingleImage(object source, ImageType imageType, string name, MainViewModel vm)
    {
        Dispatcher.UIThread.Invoke(() =>
        {
            if (vm.CurrentView != vm.ImageViewer)
            {
                vm.CurrentView = vm.ImageViewer;
            }
        }, DispatcherPriority.Render);

        vm.ImageIterator = null;
        int width, height;
        if (imageType is ImageType.Svg)
        {
            var path = source as string;
            using var magickImage = new MagickImage();
            magickImage.Ping(path);
            vm.ImageSource = source;
            vm.ImageType = ImageType.Svg;
            width = (int)magickImage.Width;
            height = (int)magickImage.Height;
        }
        else
        {
            var bitmap = source as Bitmap;
            vm.ImageSource = source;
            vm.ImageType = imageType == ImageType.Invalid ? ImageType.Bitmap : imageType;
            width = bitmap?.PixelSize.Width ?? 0;
            height = bitmap?.PixelSize.Height ?? 0;
        }

        if (GalleryFunctions.IsBottomGalleryOpen)
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                // Trigger animation
                vm.GalleryMode = GalleryMode.BottomToClosed;
            });
            // Set to closed to ensure next gallery mode changing is fired
            vm.GalleryMode = GalleryMode.Closed;
        }

        Dispatcher.UIThread.Invoke(() =>
        {
            WindowResizing.SetSize(width, height, 0, 0, 0, vm);
        });
        
        if (vm.RotationAngle != 0)
        {
            vm.ImageViewer.Rotate(vm.RotationAngle);
        }

        var singeImageWindowTitles = ImageTitleFormatter.GenerateTitleForSingleImage(width, height, name, 1);
        vm.WindowTitle = singeImageWindowTitles.TitleWithAppName;
        vm.Title = singeImageWindowTitles.BaseTitle; 
        vm.TitleTooltip = singeImageWindowTitles.BaseTitle;
        vm.GalleryMargin = new Thickness(0, 0, 0, 0);

        vm.PlatformService.StopTaskbarProgress();
        
        vm.PixelWidth = width;
        vm.PixelHeight = height;
    }
    
    public static void SetTiffImage(TiffManager.TiffNavigationInfo tiffNavigationInfo, string name, MainViewModel vm)
    {
        var source = tiffNavigationInfo.Pages[tiffNavigationInfo.CurrentPage].ToWriteableBitmap();
        vm.ImageSource = source;
        vm.SecondaryImageSource = null;
        vm.ImageType = ImageType.Bitmap;
        var width = source?.PixelSize.Width ?? 0;
        var height = source?.PixelSize.Height ?? 0;
        
        name = name.Insert(name.LastIndexOf('.'), $" [{tiffNavigationInfo.CurrentPage + 1}/{tiffNavigationInfo.PageCount}]");

        Dispatcher.UIThread.Invoke(() =>
        {
            WindowResizing.SetSize(width, height, 0, 0, 0, vm);
        }, DispatcherPriority.Send);
        
        if (vm.RotationAngle != 0)
        {
            vm.ImageViewer.Rotate(vm.RotationAngle);
        }

        var singeImageWindowTitles = ImageTitleFormatter.GenerateTitleForSingleImage(width, height, name, 1);
        vm.WindowTitle = singeImageWindowTitles.TitleWithAppName;
        vm.Title = singeImageWindowTitles.BaseTitle; 
        vm.TitleTooltip = singeImageWindowTitles.BaseTitle;
        vm.GalleryMargin = new Thickness(0, 0, 0, 0);

        vm.PlatformService.StopTaskbarProgress();
        
        vm.PixelWidth = width;
        vm.PixelHeight = height;
    }

    public static void LoadingPreview(MainViewModel vm, int index)
    {
        SetTitleHelper.SetLoadingTitle(vm);
        vm.IsLoading = true;
        
        vm.SelectedGalleryItemIndex = index;
        if (Settings.Gallery.IsBottomGalleryShown)
        {
            GalleryNavigation.CenterScrollToSelectedItem(vm);
        }

        vm.ImageSource = GetThumbnails.GetExifThumb(vm.ImageIterator.ImagePaths[index]);
        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            vm.SecondaryImageSource = GetThumbnails.GetExifThumb(vm.ImageIterator.ImagePaths[vm.ImageIterator.NextIndex]);
        }
        else
        {
            vm.SecondaryImageSource = null;
        }
    }

    #endregion
}