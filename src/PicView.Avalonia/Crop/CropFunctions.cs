﻿using Avalonia;
using Avalonia.Media.Imaging;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.Views.UC;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Gallery;
using PicView.Core.Localization;

namespace PicView.Avalonia.Crop;

public static class CropFunctions
{
    public static bool IsCropping {get; private set;} 
    
    /// <summary>
    /// Starts the cropping functionality by setting up the ImageCropperViewModel 
    /// and adding the CropControl to the main view.
    /// </summary>
    /// <param name="vm">The main view model instance containing image properties and state.</param>
    /// <remarks>
    /// This method checks if cropping can be enabled and if the image source is valid.
    /// If conditions are met, it configures the crop control with the appropriate dimensions
    /// and updates the view model's title and tooltip to reflect the cropping state.
    /// </remarks>
    public static void StartCropControl(MainViewModel vm)
    {
        if (!DetermineIfShouldBeEnabled(vm))
        {
            return;
        }
        if (vm?.ImageSource is not Bitmap bitmap)
        {
            return;
        }
        // Hide bottom gallery when entering crop mode
        if (Settings.Gallery.IsBottomGalleryShown)
        {
            vm.GalleryMode = GalleryMode.Closed;
            // Reset setting before resizing
            Settings.Gallery.IsBottomGalleryShown = false;
            WindowResizing.SetSize(vm);
            Settings.Gallery.IsBottomGalleryShown = true;
        }
        var size = new Size(vm.ImageWidth, vm.ImageHeight);
        var cropperViewModel = new ImageCropperViewModel(bitmap)
        {
            ImageWidth = size.Width,
            ImageHeight = size.Height,
            AspectRatio = vm.AspectRatio
        };
        var cropControl = new CropControl
        {
            DataContext = cropperViewModel,
            Width = size.Width,
            Height = size.Height,
            Margin = new Thickness(0)
        };
        vm.CurrentView = cropControl;
        
        IsCropping = true;
        vm.Title = TranslationHelper.Translation.CropMessage;
        vm.TitleTooltip = TranslationHelper.Translation.CropMessage;
        
        FunctionsHelper.CloseMenus();
    }
    
    public static void CloseCropControl(MainViewModel vm)
    {
        if (Settings.Gallery.IsBottomGalleryShown)
        {
            vm.GalleryMode = GalleryMode.ClosedToBottom;
            WindowResizing.SetSize(vm);
        }

        vm.CurrentView = vm.ImageViewer;
        IsCropping = false;
        SetTitleHelper.RefreshTitle(vm);
    }

    public static bool DetermineIfShouldBeEnabled(MainViewModel vm)
    {
        if (IsCropping)
        {
            return false;
        }
        if (vm?.ImageSource is not Bitmap)
        {
            return false;
        }

        if (Settings.ImageScaling.ShowImageSideBySide)
        {
            return false;
        }

        if (UIHelper.IsDialogOpen)
        {
            return false;
        }

        if (vm.IsEditableTitlebarOpen)
        {
            return false;
        }
        
        return vm is { ScaleX: 1, RotationAngle: 0 };
    }
}
