using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace PicView.Avalonia.CustomControls;

public class ThumbImage : Image
{
    protected override Size MeasureOverride(Size availableSize)
    {
        Size? size = null;
        try
        {
            size = new Size();


        if (Source != null)
        {
            size = Stretch.CalculateSize(availableSize, Source.Size, StretchDirection);
        }
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
        }

        return size ?? new Size();
    }
    
    protected override Size ArrangeOverride(Size finalSize)
    {
        try
        {
            if (Source != null)
            {
                var sourceSize = Source.Size;
                var result = Stretch.CalculateSize(finalSize, sourceSize);
                return result;
            }
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
        }
        return new Size();
    }
}