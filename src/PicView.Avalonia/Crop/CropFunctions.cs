using Avalonia;
using Avalonia.Media.Imaging;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.Views.UC;
using PicView.Core.Localization;

namespace PicView.Avalonia.Crop;

public static class CropFunctions
{
    public static bool IsCropping {get; private set;} 
    
    /// <summary>
    /// Initializes the cropping functionality by setting up the ImageCropperViewModel 
    /// and adding the CropControl to the main view.
    /// </summary>
    /// <param name="vm">The main view model instance containing image properties and state.</param>
    /// <remarks>
    /// This method checks if cropping can be enabled and if the image source is valid.
    /// If conditions are met, it configures the crop control with the appropriate dimensions
    /// and updates the view model's title and tooltip to reflect the cropping state.
    /// </remarks>
    public static void Init(MainViewModel vm)
    {
        if (!DetermineIfShouldBeEnabled(vm))
        {
            return;
        }
        if (vm?.ImageSource is not Bitmap bitmap)
        {
            return;
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
        };
        UIHelper.GetMainView.MainGrid.Children.Add(cropControl);
        
        IsCropping = true;
        vm.Title = TranslationHelper.Translation.CropMessage;
        vm.TitleTooltip = TranslationHelper.Translation.CropMessage;
    }
    
    public static void CloseCropControl(MainViewModel vm)
    {
        UIHelper.GetMainView.MainGrid.Children.Remove(UIHelper.GetMainView.MainGrid.Children.OfType<CropControl>().First());
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
