using Avalonia;
using Avalonia.Media.Imaging;

namespace PicView.Avalonia.ImageHandling;

public static class BitmapHelper
{
    public static Bitmap ConvertCroppedBitmapToBitmap(CroppedBitmap croppedBitmap)
    {
        var renderTargetBitmap = new RenderTargetBitmap(
            new PixelSize((int)croppedBitmap.Size.Width, (int)croppedBitmap.Size.Height),
            new Vector(96, 96));

        using var context = renderTargetBitmap.CreateDrawingContext();
        context.DrawImage(croppedBitmap, new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height), new Rect(0, 0, croppedBitmap.Size.Width, croppedBitmap.Size.Height));

        return renderTargetBitmap;
    }
}
