using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using PicView.Avalonia.ViewModels;

namespace PicView.Avalonia.Views.UC;

public partial class CropControl : UserControl
{
    private Point _dragStart;
    private bool _isDragging;
    private bool _isResizing;
    private Rect _originalRect;
    private Point _resizeStart;

    public CropControl()
    {
        InitializeComponent();
        Loaded += delegate
        {
            InitializeLayout();
            MainRectangle.PointerPressed += OnPointerPressed;
            MainRectangle.PointerReleased += OnPointerReleased;
            MainRectangle.PointerMoved += OnPointerMoved;

            TopLeftButton.PointerPressed += OnResizePointerPressed;
            TopRightButton.PointerPressed += OnResizePointerPressed;
            BottomLeftButton.PointerPressed += OnResizePointerPressed;
            BottomRightButton.PointerPressed += OnResizePointerPressed;
            LeftMiddleButton.PointerPressed += OnResizePointerPressed;
            RightMiddleButton.PointerPressed += OnResizePointerPressed;
            TopMiddleButton.PointerPressed += OnResizePointerPressed;
            BottomMiddleButton.PointerPressed += OnResizePointerPressed;

            TopLeftButton.PointerMoved += (_, e) => ResizeTopLeft(e);
            TopRightButton.PointerMoved += (_, e) => ResizeTopRight(e);
            BottomLeftButton.PointerMoved += (_, e) => ResizeBottomLeft(e);
            BottomRightButton.PointerMoved += (_, e) => ResizeBottomRight(e);
            LeftMiddleButton.PointerMoved += (_, e) => ResizeLeftMiddle(e);
            RightMiddleButton.PointerMoved += (_, e) => ResizeRightMiddle(e);
            TopMiddleButton.PointerMoved += (_, e) => ResizeTopMiddle(e);
            BottomMiddleButton.PointerMoved += (_, e) => ResizeBottomMiddle(e);

            TopLeftButton.PointerReleased += OnResizePointerReleased;
            TopRightButton.PointerReleased += OnResizePointerReleased;
            BottomLeftButton.PointerReleased += OnResizePointerReleased;
            BottomRightButton.PointerReleased += OnResizePointerReleased;
            LeftMiddleButton.PointerReleased += OnResizePointerReleased;
            RightMiddleButton.PointerReleased += OnResizePointerReleased;
            TopMiddleButton.PointerReleased += OnResizePointerReleased;
            BottomMiddleButton.PointerReleased += OnResizePointerReleased;
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

        // Set buttons positions based on MainRectangle's position
        UpdateButtonPositions(vm.SelectionX, vm.SelectionY, vm.SelectionWidth, vm.SelectionHeight);

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

    private void OnResizePointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (DataContext is not ImageCropperViewModel vm || !e.GetCurrentPoint(this).Properties.IsLeftButtonPressed)
        {
            return;
        }

        // Capture the start position of the mouse and the initial size/position of the rectangle
        _resizeStart = e.GetPosition(RootCanvas);
        _originalRect = new Rect(Canvas.GetLeft(MainRectangle), Canvas.GetTop(MainRectangle), vm.SelectionWidth,
            vm.SelectionHeight);

        _isResizing = true;
    }

    private void UpdateButtonPositions(double selectionX, double selectionY, double selectionWidth,
        double selectionHeight)
    {
        try
        {
            // Get the bounds of the RootCanvas (the control container)
            const int rootCanvasLeft = 0;
            const int rootCanvasTop = 0;
            var rootCanvasRight = RootCanvas.Bounds.Width;
            var rootCanvasBottom = RootCanvas.Bounds.Height;

            // Calculate the positions for each button
            var topLeftX = selectionX - TopLeftButton.Width / 2;
            var topLeftY = selectionY - TopLeftButton.Height / 2;

            var topRightX = selectionX + selectionWidth - TopRightButton.Width / 2;
            var topRightY = selectionY - TopRightButton.Height / 2;

            var topMiddleX = selectionX + selectionWidth / 2 - TopMiddleButton.Width / 2;
            var topMiddleY = selectionY - TopMiddleButton.Height / 2;

            var bottomLeftX = selectionX - BottomLeftButton.Width / 2;
            var bottomLeftY = selectionY + selectionHeight - BottomLeftButton.Height / 2;

            var bottomRightX = selectionX + selectionWidth - BottomRightButton.Width / 2;
            var bottomRightY = selectionY + selectionHeight - BottomRightButton.Height / 2;

            var bottomMiddleX = selectionX + selectionWidth / 2 - BottomMiddleButton.Width / 2;
            var bottomMiddleY = selectionY + selectionHeight - BottomMiddleButton.Height / 2;

            var leftMiddleX = selectionX - LeftMiddleButton.Width / 2;
            var leftMiddleY = selectionY + selectionHeight / 2 - LeftMiddleButton.Height / 2;

            var rightMiddleX = selectionX + selectionWidth - RightMiddleButton.Width / 2;
            var rightMiddleY = selectionY + selectionHeight / 2 - RightMiddleButton.Height / 2;

            // Ensure buttons stay within RootCanvas bounds (by clamping positions)
            topLeftX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - TopLeftButton.Width, topLeftX));
            topLeftY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - TopLeftButton.Height, topLeftY));

            topRightX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - TopRightButton.Width, topRightX));
            topRightY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - TopRightButton.Height, topRightY));

            topMiddleX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - TopMiddleButton.Width, topMiddleX));
            topMiddleY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - TopMiddleButton.Height, topMiddleY));

            bottomLeftX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - BottomLeftButton.Width, bottomLeftX));
            bottomLeftY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - BottomLeftButton.Height, bottomLeftY));

            bottomRightX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - BottomRightButton.Width, bottomRightX));
            bottomRightY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - BottomRightButton.Height, bottomRightY));

            bottomMiddleX = Math.Max(rootCanvasLeft,
                Math.Min(rootCanvasRight - BottomMiddleButton.Width, bottomMiddleX));
            bottomMiddleY = Math.Max(rootCanvasTop,
                Math.Min(rootCanvasBottom - BottomMiddleButton.Height, bottomMiddleY));

            leftMiddleX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - LeftMiddleButton.Width, leftMiddleX));
            leftMiddleY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - LeftMiddleButton.Height, leftMiddleY));

            rightMiddleX = Math.Max(rootCanvasLeft, Math.Min(rootCanvasRight - RightMiddleButton.Width, rightMiddleX));
            rightMiddleY = Math.Max(rootCanvasTop, Math.Min(rootCanvasBottom - RightMiddleButton.Height, rightMiddleY));

            // Set the final button positions
            Canvas.SetLeft(TopLeftButton, topLeftX);
            Canvas.SetTop(TopLeftButton, topLeftY);

            Canvas.SetLeft(TopRightButton, topRightX);
            Canvas.SetTop(TopRightButton, topRightY);

            Canvas.SetLeft(TopMiddleButton, topMiddleX);
            Canvas.SetTop(TopMiddleButton, topMiddleY);

            Canvas.SetLeft(BottomLeftButton, bottomLeftX);
            Canvas.SetTop(BottomLeftButton, bottomLeftY);

            Canvas.SetLeft(BottomRightButton, bottomRightX);
            Canvas.SetTop(BottomRightButton, bottomRightY);

            Canvas.SetLeft(BottomMiddleButton, bottomMiddleX);
            Canvas.SetTop(BottomMiddleButton, bottomMiddleY);

            Canvas.SetLeft(LeftMiddleButton, leftMiddleX);
            Canvas.SetTop(LeftMiddleButton, leftMiddleY);

            Canvas.SetLeft(RightMiddleButton, rightMiddleX);
            Canvas.SetTop(RightMiddleButton, rightMiddleY);
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
        var bottom = Convert.ToInt32(top + vm.SelectionHeight);

        // Calculate the positions and sizes for the surrounding rectangles
        // Top Rectangle (above MainRectangle)
        TopRectangle.Width = vm.ImageWidth;
        TopRectangle.Height = top < 0 ? 0 : top;
        Canvas.SetTop(TopRectangle, 0);

        // Bottom Rectangle (below MainRectangle)
        BottomRectangle.Width = vm.ImageWidth;
        var newBottomRectangleHeight = vm.ImageHeight - bottom < 0 ? 0 : vm.ImageHeight - bottom;
        BottomRectangle.Height = newBottomRectangleHeight;
        Canvas.SetTop(BottomRectangle, bottom);

        // Left Rectangle (left of MainRectangle)
        LeftRectangle.Width = left < 0 ? 0 : left;
        LeftRectangle.Height = vm.SelectionHeight;
        Canvas.SetLeft(LeftRectangle, 0);
        Canvas.SetTop(LeftRectangle, top);

        // Right Rectangle (right of MainRectangle)
        var newRightRectangleWidth = vm.ImageWidth - right < 0 ? 0 : vm.ImageWidth - right;
        RightRectangle.Width = newRightRectangleWidth;
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
            UpdateButtonPositions(newLeft, newTop, vm.SelectionWidth, vm.SelectionHeight);
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

    private void ResizeTopLeft(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new width and height based on the drag delta
        var newWidth = _originalRect.Width - delta.X;
        var newHeight = _originalRect.Height - delta.Y;

        // Ensure the rectangle stays within the canvas bounds
        var newLeft = Math.Max(_originalRect.X + delta.X, 0);
        var newTop = Math.Max(_originalRect.Y + delta.Y, 0);

        // Constrain the new width and height to not exceed the bounds
        newWidth = Math.Max(newWidth, 1); // Prevent width from becoming 0 or negative
        newHeight = Math.Max(newHeight, 1); // Prevent height from becoming 0 or negative

        // Ensure the right and bottom edges don't go beyond the canvas
        if (newLeft + newWidth > vm.ImageWidth)
        {
            newWidth = vm.ImageWidth - newLeft;
        }

        if (newTop + newHeight > vm.ImageHeight)
        {
            newHeight = vm.ImageHeight - newTop;
        }

        if (vm.SelectionX is 0 && vm.SelectionY is 0 && newLeft is 0 && newTop is 0)
        {
            return;
        }

        // Apply the new size and position
        vm.SelectionX = newLeft;
        vm.SelectionY = newTop;
        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        Canvas.SetLeft(MainRectangle, newLeft);
        Canvas.SetTop(MainRectangle, newTop);

        UpdateButtonPositions(vm.SelectionX, vm.SelectionY, vm.SelectionWidth, vm.SelectionHeight);
        UpdateSurroundingRectangles();
    }

    private void ResizeTopRight(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate new width and height
        var newWidth = _originalRect.Width + delta.X;
        var newHeight = _originalRect.Height - delta.Y;
        var newY = _originalRect.Y + delta.Y;

        // Ensure the width doesn't exceed the canvas' right edge
        if (_originalRect.X + newWidth > vm.ImageWidth)
        {
            newWidth = vm.ImageWidth - _originalRect.X;
        }

        // Ensure the top doesn't move above the top edge of the canvas
        if (newY < 0)
        {
            newHeight = _originalRect.Height + _originalRect.Y; // Shrink height by the amount moved up
            newY = 0;
        }

        // Prevent the height from becoming too small
        newHeight = Math.Max(newHeight, 1);

        // Apply the new size and position
        vm.SelectionY = newY;
        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        Canvas.SetLeft(MainRectangle, _originalRect.X);
        Canvas.SetTop(MainRectangle, newY);

        UpdateButtonPositions(_originalRect.X, newY, newWidth, newHeight);
        UpdateSurroundingRectangles();
    }


    private void ResizeBottomLeft(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        var newWidth = _originalRect.Width - delta.X;
        var newHeight = _originalRect.Height + delta.Y;
        var newX = _originalRect.X + delta.X;

        // Ensure the left doesn't move beyond the left edge
        if (newX < 0)
        {
            newWidth = _originalRect.Width + _originalRect.X; // Shrink width by the amount moved left
            newX = 0;
        }

        // Ensure the height doesn't exceed the canvas' bottom edge
        if (_originalRect.Y + newHeight > vm.ImageHeight)
        {
            newHeight = vm.ImageHeight - _originalRect.Y;
        }

        // Prevent the width and height from becoming too small
        newWidth = Math.Max(newWidth, 1);
        newHeight = Math.Max(newHeight, 1);

        // Apply the new size and position
        vm.SelectionX = newX;
        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        Canvas.SetLeft(MainRectangle, newX);
        Canvas.SetTop(MainRectangle, _originalRect.Y);

        UpdateButtonPositions(newX, _originalRect.Y, newWidth, newHeight);
        UpdateSurroundingRectangles();
    }


    private void ResizeBottomRight(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new width and height based on the drag delta
        var newWidth = _originalRect.Width + delta.X;
        var newHeight = _originalRect.Height + delta.Y;

        // Ensure the new width and height do not exceed the image bounds
        var newRight = _originalRect.X + newWidth;
        var newBottom = _originalRect.Y + newHeight;

        if (newRight > vm.ImageWidth)
        {
            newWidth = vm.ImageWidth - _originalRect.X;
        }

        if (newBottom > vm.ImageHeight)
        {
            newHeight = vm.ImageHeight - _originalRect.Y;
        }

        // Constrain the minimum size
        newWidth = Math.Max(newWidth, 1);
        newHeight = Math.Max(newHeight, 1);

        // Apply the new width and height
        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;

        UpdateButtonPositions(vm.SelectionX, vm.SelectionY, vm.SelectionWidth, vm.SelectionHeight);
        UpdateSurroundingRectangles();
    }

    private void ResizeLeftMiddle(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Get the current mouse position relative to RootCanvas
        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new X position
        var newX = _originalRect.X + delta.X;

        // Calculate the new width based on horizontal movement
        var newWidth = _originalRect.Width - delta.X;

        // Ensure that the rectangle doesn't go beyond the right boundary
        if (newX < 0)
        {
            newWidth = _originalRect.Width + _originalRect.X;
            newX = 0;
        }

        // Ensure the new width doesn't go negative or too small
        newWidth = Math.Max(newWidth, 1);

        // Update the view model with the new X position and width
        vm.SelectionX = newX;
        vm.SelectionWidth = newWidth;

        // Update the rectangle on the canvas
        Canvas.SetLeft(MainRectangle, newX);
        Canvas.SetTop(MainRectangle, _originalRect.Y);

        // Update buttons and surrounding rectangles
        UpdateButtonPositions(newX, _originalRect.Y, vm.SelectionWidth, vm.SelectionHeight);
        UpdateSurroundingRectangles();
    }


    private void ResizeRightMiddle(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Get the current mouse position relative to RootCanvas
        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new width based on horizontal movement
        var newWidth = _originalRect.Width + delta.X;

        // Ensure the new width doesn't go beyond the bounds of the RootCanvas
        if (_originalRect.X + newWidth > vm.ImageWidth)
        {
            newWidth = vm.ImageWidth - _originalRect.X;
        }

        // Constrain the width to a minimum value (e.g., 1 to prevent zero or negative width)
        newWidth = Math.Max(newWidth, 1);

        // Update the view model with the new width
        vm.SelectionWidth = newWidth;

        // Update the rectangle on the canvas
        Canvas.SetLeft(MainRectangle, _originalRect.X);
        Canvas.SetTop(MainRectangle, _originalRect.Y);

        // Update buttons and surrounding rectangles
        UpdateButtonPositions(_originalRect.X, _originalRect.Y, vm.SelectionWidth, vm.SelectionHeight);
        UpdateSurroundingRectangles();
    }

    private void ResizeTopMiddle(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Get the current mouse position relative to RootCanvas
        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new top position and height
        var newTop = _originalRect.Y + delta.Y;
        var newHeight = _originalRect.Height - delta.Y;

        // Ensure the new height doesn't go negative or too small
        if (newHeight < 1)
        {
            newHeight = 1;
            newTop = _originalRect.Y + (_originalRect.Height - 1); // Adjust top to preserve min height
        }

        // Ensure the top edge doesn't go beyond the canvas bounds
        if (newTop < 0)
        {
            newTop = 0;
            newHeight = _originalRect.Height + _originalRect.Y; // Adjust height to compensate
        }

        // Update the view model with the new top and height
        vm.SelectionHeight = newHeight;
        vm.SelectionY = newTop;

        // Update the rectangle on the canvas
        Canvas.SetLeft(MainRectangle, _originalRect.X);
        Canvas.SetTop(MainRectangle, newTop);

        // Update buttons and surrounding rectangles
        UpdateButtonPositions(_originalRect.X, newTop, vm.SelectionWidth, newHeight);
        UpdateSurroundingRectangles();
    }


    private void ResizeBottomMiddle(PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        // Get the current mouse position relative to RootCanvas
        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Calculate the new height based on vertical movement
        var newHeight = _originalRect.Height + delta.Y;

        // Ensure the new height doesn't go negative or too small
        newHeight = Math.Max(newHeight, 1);

        // Ensure the bottom edge doesn't go beyond the canvas bounds
        if (_originalRect.Y + newHeight > RootCanvas.Bounds.Height)
        {
            newHeight = RootCanvas.Bounds.Height - _originalRect.Y;
        }

        // Update the view model with the new height
        vm.SelectionHeight = newHeight;

        // Update the rectangle on the canvas
        Canvas.SetLeft(MainRectangle, _originalRect.X);
        Canvas.SetTop(MainRectangle, _originalRect.Y);

        // Update buttons and surrounding rectangles
        UpdateButtonPositions(_originalRect.X, _originalRect.Y, vm.SelectionWidth, newHeight);
        UpdateSurroundingRectangles();
    }

    private void OnResizePointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isResizing = false;
    }
}