using Avalonia;
using Avalonia.Media.Imaging;
using ReactiveUI;

namespace PicView.Avalonia.ViewModels;

public class ImageCropperViewModel(Bitmap bitmap) : ViewModelBase
{
    public Bitmap Bitmap
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    } = bitmap;

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
    
    public double BottomOverlayHeight
    {
        get => ImageHeight - (SelectionY + SelectionHeight);
    }

    public double RightOverlayWidth
    {
        get => ImageWidth - (SelectionX + SelectionWidth);
    }

    // Call this method when the user completes the selection
    public CroppedBitmap GetCroppedBitmap()
    {
        var sourceRect = new PixelRect((int)SelectionX, (int)SelectionY, (int)SelectionWidth, (int)SelectionHeight);
        var croppedBitmap = new CroppedBitmap(Bitmap, sourceRect);
        return croppedBitmap;
    }
}
