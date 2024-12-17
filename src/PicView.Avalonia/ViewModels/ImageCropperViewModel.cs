using System.Reactive;
using Avalonia;
using Avalonia.Media.Imaging;
using PicView.Avalonia.Crop;
using PicView.Avalonia.UI;
using PicView.Core.Localization;
using ReactiveUI;

namespace PicView.Avalonia.ViewModels;

public class ImageCropperViewModel : ViewModelBase
{
    public ImageCropperViewModel(Bitmap bitmap)
    {
        Bitmap = bitmap;
        CropImageCommand  = ReactiveCommand.CreateFromTask(async () =>
        {
            
        });
        CloseCropCommand  = ReactiveCommand.Create(() =>
        {
            if (UIHelper.GetMainView.DataContext is not MainViewModel vm)
            {
                return;
            }
            CropFunctions.CloseCropControl(vm);
        });
        Crop = TranslationHelper.Translation.CropPicture;
        Close = TranslationHelper.Translation.Close;
    }
    
    public ReactiveCommand<Unit, Unit>? CropImageCommand { get; }
    
    public ReactiveCommand<Unit, Unit>? CloseCropCommand { get; }
    
    public Bitmap Bitmap
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double SelectionX
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double SelectionY
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public double SelectionWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = 100;

    public double SelectionHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = 100;
    
    public double ImageWidth
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }
    public double ImageHeight
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    // Call this method when the user completes the selection
    public CroppedBitmap GetCroppedBitmap()
    {
        var sourceRect = new PixelRect((int)SelectionX, (int)SelectionY, (int)SelectionWidth, (int)SelectionHeight);
        var croppedBitmap = new CroppedBitmap(Bitmap, sourceRect);
        return croppedBitmap;
    }
}
