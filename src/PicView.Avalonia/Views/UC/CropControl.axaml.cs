using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using PicView.Avalonia.ViewModels;

namespace PicView.Avalonia.Views.UC;

public partial class CropControl : UserControl
{
    private Point _dragStart;
    private bool _isDragging;
    private Rect _originalRect;

    public CropControl()
    {
        InitializeComponent();
        Loaded += delegate
        {
            InitializeLayout();
            MainRectangle.PointerPressed += OnPointerPressed;
            MainRectangle.PointerReleased += OnPointerReleased;
            MainRectangle.PointerMoved += OnPointerMoved;
        };
    }

    private void InitializeLayout()
    {
        if (DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Ensure image dimensions are valid before proceeding
        if (vm.ImageWidth <= 0 || vm.ImageHeight <= 0)
        {
            return;
        }

        // Set initial width and height for the crop rectangle
        vm.SelectionWidth = 200;
        vm.SelectionHeight = 200;

        // Calculate centered position
        vm.SelectionX = (vm.ImageWidth - vm.SelectionWidth) / 2;
        vm.SelectionY = (vm.ImageHeight - vm.SelectionHeight) / 2;

        // Apply the calculated position to the MainRectangle
        Canvas.SetLeft(MainRectangle, vm.SelectionX);
        Canvas.SetTop(MainRectangle, vm.SelectionY);

        try
        {
            UpdateSurroundingRectangles();
        }
        catch (Exception e)
        {
            #if DEBUG
            Console.WriteLine(e);
            #endif
        }
    }

    private void UpdateSurroundingRectangles()
    {
        if (DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Converting to int fixes black border
        var left = Convert.ToInt32(Canvas.GetLeft(MainRectangle));
        var top = Convert.ToInt32(Canvas.GetTop(MainRectangle));
        var right = Convert.ToInt32(left + vm.SelectionWidth);
        var bottom= Convert.ToInt32(top + vm.SelectionHeight);

        // Ensure the left and top values are not negative
        left = left < 0 ? 0 : left;
        top = top < 0 ? 0 : top;

        // Calculate the positions and sizes for the surrounding rectangles
        // Top Rectangle (above MainRectangle)
        TopRectangle.Width = vm.ImageWidth;
        TopRectangle.Height = top;
        Canvas.SetTop(TopRectangle, 0);

        // Bottom Rectangle (below MainRectangle)
        BottomRectangle.Width = vm.ImageWidth;
        BottomRectangle.Height = vm.ImageHeight - bottom;
        Canvas.SetTop(BottomRectangle, bottom);

        // Left Rectangle (left of MainRectangle)
        LeftRectangle.Width = left;
        LeftRectangle.Height = vm.SelectionHeight;
        Canvas.SetLeft(LeftRectangle, 0);
        Canvas.SetTop(LeftRectangle, top);

        // Right Rectangle (right of MainRectangle)
        RightRectangle.Width = vm.ImageWidth - right;
        RightRectangle.Height = vm.SelectionHeight;
        Canvas.SetLeft(RightRectangle, right);
        Canvas.SetTop(RightRectangle, top);
    }

    private void OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            return;
        }

        if (DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        _dragStart = e.GetPosition(RootCanvas); // Make sure to get position relative to RootCanvas

        // Get current left and top values; ensure they are initialized
        var currentLeft = Canvas.GetLeft(MainRectangle);
        var currentTop = Canvas.GetTop(MainRectangle);

        // Set default values if NaN
        if (double.IsNaN(currentLeft))
        {
            currentLeft = 0;
        }

        if (double.IsNaN(currentTop))
        {
            currentTop = 0;
        }

        _originalRect = new Rect(currentLeft, currentTop, vm.SelectionWidth, vm.SelectionHeight);
        _isDragging = true;
    }

    private void OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        if (!_isDragging)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas); // Ensure it's relative to RootCanvas
        var delta = currentPos - _dragStart;

        // Calculate new left and top positions, ensure _originalRect is valid
        var newLeft = _originalRect.X + delta.X;
        var newTop = _originalRect.Y + delta.Y;

        // Clamp the newLeft and newTop values to keep the rectangle within bounds
        newLeft = Math.Max(0, Math.Min(vm.ImageWidth - vm.SelectionWidth, newLeft));
        newTop = Math.Max(0, Math.Min(vm.ImageHeight - vm.SelectionHeight, newTop));

        // Only proceed if new positions are valid (i.e., not NaN)
        if (double.IsNaN(newLeft) || double.IsNaN(newTop))
        {
            return;
        }

        // Update the main rectangle's position
        Canvas.SetLeft(MainRectangle, newLeft);
        Canvas.SetTop(MainRectangle, newTop);

        // Update view model values
        vm.SelectionX = newLeft;
        vm.SelectionY = newTop;

        // Update the surrounding rectangles to fill the space
        try
        {
            UpdateSurroundingRectangles();
        }
        catch (Exception exception)
        {
#if DEBUG
            Console.WriteLine(exception);
#endif
        }
    }


    private void OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isDragging = false;
    }
}