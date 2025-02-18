﻿using Avalonia;
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
    /// <summary>
    ///     Updates the image source in the main view model based on the specified index and preloaded values.
    /// </summary>
    /// <param name="vm">The main view model instance.</param>
    /// <param name="index">The index of the image to update.</param>
    /// <param name="imagePaths">The list of image paths to navigate through.</param>
    /// <param name="isReversed">Indicates if the navigation is in reverse order.</param>
    /// <param name="preLoadValue">The preloaded value of the current image.</param>
    /// <param name="nextPreloadValue">Optional: The preloaded value of the next image, used for side-by-side display.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
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
            if (TiffManager.IsTiff(preLoadValue.ImageModel.FileInfo.FullName))
            {
                if (TiffManager.IsTiff(preLoadValue.ImageModel.FileInfo.FullName))
                {
                    SetTitleHelper.TrySetTiffTitle(preLoadValue.ImageModel.PixelWidth,
                        preLoadValue.ImageModel.PixelHeight, vm.ImageIterator.CurrentIndex,
                        preLoadValue.ImageModel.FileInfo, vm);
                }
                else
                {
                    SetTitleHelper.SetTitle(vm, preLoadValue.ImageModel);
                }
            }
            else
            {
                SetTitleHelper.SetTitle(vm, preLoadValue.ImageModel);
            }
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

        SetStats(vm, index, preLoadValue.ImageModel);
    }

    #region Single Image

    /// <summary>
    ///     Sets the given image as the single image displayed in the view.
    /// </summary>
    /// <param name="source">The source of the image to display.</param>
    /// <param name="imageType"></param>
    /// <param name="name">The name of the image.</param>
    /// <param name="vm">The main view model instance.</param>
    public static void SetSingleImage(object source, ImageType imageType, string name, MainViewModel vm)
    {
        ExecuteSetSingleImageAsync(
            source,
            imageType,
            name,
            vm,
            (action, priority) =>
            {
                Dispatcher.UIThread.Invoke(action, priority);
                return Task.CompletedTask;
            }).GetAwaiter().GetResult();
    }

    /// <inheritdoc cref="SetSingleImage" />
    public static async Task SetSingleImageAsync(object source, ImageType imageType, string name, MainViewModel vm)
    {
        await ExecuteSetSingleImageAsync(
            source,
            imageType,
            name,
            vm,
            async (action, priority) => { await Dispatcher.UIThread.InvokeAsync(action, priority); });
    }

    /// <summary>
    ///     Internal method that sets a single image as the source of the image viewer.
    /// </summary>
    /// <param name="source">The source of the image to display.</param>
    /// <param name="imageType">The type of the image.</param>
    /// <param name="name">The name of the image.</param>
    /// <param name="vm">The main view model instance.</param>
    /// <param name="dispatchAction">A function that dispatches an action to the UI thread.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task ExecuteSetSingleImageAsync(
        object source,
        ImageType imageType,
        string name,
        MainViewModel vm,
        Func<Action, DispatcherPriority, Task> dispatchAction)
    {
        await dispatchAction(() =>
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
            await dispatchAction(() => { vm.GalleryMode = GalleryMode.BottomToClosed; }, DispatcherPriority.Render);
            vm.GalleryMode = GalleryMode.Closed;
        }

        await dispatchAction(() => { WindowResizing.SetSize(width, height, 0, 0, 0, vm); }, DispatcherPriority.Render);

        var singeImageWindowTitles = ImageTitleFormatter.GenerateTitleForSingleImage(width, height, name, 1);
        vm.WindowTitle = singeImageWindowTitles.TitleWithAppName;
        vm.Title = singeImageWindowTitles.BaseTitle;
        vm.TitleTooltip = singeImageWindowTitles.BaseTitle;
        vm.GalleryMargin = new Thickness(0, 0, 0, 0);

        vm.PlatformService.StopTaskbarProgress();

        vm.PixelWidth = width;
        vm.PixelHeight = height;

        await dispatchAction(() => { UIHelper.GetGalleryView.IsVisible = false; }, DispatcherPriority.Render);
    }

    #endregion
    
    #region TIFF

    /// <summary>
    ///     Sets the image displayed in the view to the given TIFF image based on the given navigation info.
    /// </summary>
    /// <param name="tiffNavigationInfo">The navigation info for the TIFF image.</param>
    /// <param name="index">The index of the image to display.</param>
    /// <param name="fileInfo">The FileInfo object representing the file containing the image.</param>
    /// <param name="vm">The main view model instance.</param>
    public static void SetTiffImage(TiffManager.TiffNavigationInfo tiffNavigationInfo, int index, FileInfo fileInfo,
        MainViewModel vm)
    {
        var source = tiffNavigationInfo.Pages[tiffNavigationInfo.CurrentPage].ToWriteableBitmap();
        vm.ImageSource = source;
        vm.SecondaryImageSource = null;
        vm.ImageType = ImageType.Bitmap;
        var width = source?.PixelSize.Width ?? 0;
        var height = source?.PixelSize.Height ?? 0;

        Dispatcher.UIThread.Invoke(() => { WindowResizing.SetSize(width, height, 0, 0, 0, vm); },
            DispatcherPriority.Send);

        if (vm.RotationAngle != 0)
        {
            vm.ImageViewer.Rotate(vm.RotationAngle);
        }

        SetTitleHelper.SetTiffTitle(tiffNavigationInfo, width, height, index, fileInfo, vm);

        vm.PixelWidth = width;
        vm.PixelHeight = height;
    }
    
    #endregion

    public static void SetStats(MainViewModel vm, int index, ImageModel imageModel)
    {
        vm.PixelWidth = imageModel.PixelWidth;
        vm.PixelHeight = imageModel.PixelHeight;
        vm.GetIndex = index + 1;
        vm.ExifOrientation = imageModel.EXIFOrientation;
        vm.FileInfo = imageModel.FileInfo;
        vm.ZoomValue = 1;

        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            // Fixes incorrect rendering in the side by side view
            // TODO: Improve and fix side by side and remove this hack 
            Dispatcher.UIThread.Post(() => { vm.ImageViewer?.MainImage?.InvalidateVisual(); });
        }

        // Reset effects
        vm.EffectConfig = null;
    }

    /// <summary>
    ///     Shows the preview of the image at the given index and sets the view model to a loading state.
    /// </summary>
    /// <param name="vm">The main view model instance.</param>
    /// <param name="index">The index of the image to preview.</param>
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
            vm.SecondaryImageSource =
                GetThumbnails.GetExifThumb(vm.ImageIterator.ImagePaths[vm.ImageIterator.NextIndex]);
        }
        else
        {
            vm.SecondaryImageSource = null;
        }
    }
}