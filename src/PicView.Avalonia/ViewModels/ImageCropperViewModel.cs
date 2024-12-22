using System.Reactive;
using Avalonia.Media.Imaging;
using ImageMagick;
using PicView.Avalonia.Crop;
using PicView.Avalonia.FileSystem;
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
            await SaveCroppedImageAsync();
            
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

    public int SelectionX
    {
        get;
        set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public int SelectionY
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

    private async Task SaveCroppedImageAsync()
    {
        if (UIHelper.GetMainView.DataContext is not MainViewModel vm)
        {
            return;
        }

        string fileName;
        FileInfo fileInfo;
        if (vm.ImageIterator?.ImagePaths is null ||
            vm.ImageIterator?.ImagePaths.Count < 0)
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
        
        var image = new MagickImage(fileInfo.FullName);
        // Apply aspect ratio
        var x = Convert.ToInt32(SelectionX / AspectRatio);
        var y = Convert.ToInt32(SelectionY / AspectRatio);

        var crop = new MagickGeometry(x, y, (uint)SelectionWidth, (uint)SelectionHeight);
        image.Crop(crop);
        await image.WriteAsync(saveFileDialog);
        //await NavigationHelper.LoadPicFromFile(fileInfo.FullName, vm);
        CropFunctions.CloseCropControl(vm);
    }
}
