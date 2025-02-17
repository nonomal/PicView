using Avalonia.Threading;
using PicView.Avalonia.ViewModels;

namespace PicView.Avalonia.ImageHandling;

public static class ImageControl
{
    public static void Flip(MainViewModel vm)
    {
        if (vm.ScaleX == 1)
        {
            vm.ScaleX = -1;
            vm.GetIsFlippedTranslation = vm.UnFlip;
        }
        else
        {
            vm.ScaleX = 1;
            vm.GetIsFlippedTranslation = vm.Flip;
        }

        Dispatcher.UIThread.Invoke(() => { vm.ImageViewer.Flip(true); });
    }
}
