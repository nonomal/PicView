using System.Reactive;
using Avalonia;
using Avalonia.Media.Imaging;
using PicView.Avalonia.Crop;
using PicView.Avalonia.FileSystem;
using PicView.Avalonia.Navigation;
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
            var croppedBitmap = GetCroppedBitmap();
            await SaveCroppedImageAsync(croppedBitmap);
            
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
    
    public double AspectRatio
    {
        get;
        init => this.RaiseAndSetIfChanged(ref field, value);
    }

    public CroppedBitmap GetCroppedBitmap()
    {
        var sourceRect = new PixelRect((int)SelectionX, (int)SelectionY, (int)SelectionWidth, (int)SelectionHeight);
        var croppedBitmap = new CroppedBitmap(Bitmap, sourceRect);
        return croppedBitmap;
    }

    private async Task SaveCroppedImageAsync(CroppedBitmap croppedBitmap)
    {
        if (UIHelper.GetMainView.DataContext is not MainViewModel vm)
        {
            return;
        }

        string fileName;
        FileInfo fileInfo;
        if (!NavigationHelper.CanNavigate(vm))
        {
            var random = new Random();
            fileName = $"{TranslationHelper.Translation.Crop} {random.Next(9999)}.png";
            fileInfo = new FileInfo(fileName);
        }
        else
        {
            fileName = vm.FileInfo.FullName;
            fileInfo = vm.FileInfo;
        }

        var saveFileDialog = await FilePicker.PickFileForSavingAsync(fileName);
        if (saveFileDialog is null)
        {
            return;
        }
    }
}
