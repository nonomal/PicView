using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;

namespace PicView.Avalonia.Views.UC;

public partial class CropControl : UserControl
{
    private Point _dragStart;
    private bool _isDragging;
    private Rect _originalRect;
    private Point _resizeStart;
    private bool _isResizing;

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
            
            TopLeftButton.PointerMoved += OnResizePointerMoved;
            TopRightButton.PointerMoved += OnResizePointerMoved;
            BottomLeftButton.PointerMoved += OnResizePointerMoved;
            BottomRightButton.PointerMoved += OnResizePointerMoved;
            LeftMiddleButton.PointerMoved += OnResizePointerMoved;
            RightMiddleButton.PointerMoved += OnResizePointerMoved;
            TopMiddleButton.PointerMoved += OnResizePointerMoved;
            BottomMiddleButton.PointerMoved += OnResizePointerMoved;
            
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
        _originalRect = new Rect(Canvas.GetLeft(MainRectangle), Canvas.GetTop(MainRectangle), vm.SelectionWidth, vm.SelectionHeight);

        _isResizing = true;

        // Determine which button is being dragged
        if (sender is Border border)
        {
            // Store the button being used for resizing (you can check button name later in OnResizePointerMoved)
            border.Tag = border;
        }
    }
    
    private void OnResizePointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_isResizing || DataContext is not ImageCropperViewModel vm)
        {
            return;
        }

        var currentPos = e.GetPosition(RootCanvas);
        var delta = currentPos - _resizeStart;

        // Adjust based on the button
        if (sender is Border border)
        {
            var buttonName = border.Name;
            switch (buttonName)
            {
                case "TopLeftButton":
                    ResizeTopLeft(delta, ref vm);
                    break;
                case "TopRightButton":
                    ResizeTopRight(delta, ref vm);
                    break;
                case "BottomLeftButton":
                    ResizeBottomLeft(delta, ref vm);
                    break;
                case "BottomRightButton":
                    ResizeBottomRight(delta, ref vm);
                    break;
                case "LeftMiddleButton":
                    ResizeLeftMiddle(delta, ref vm);
                    break;
                case "RightMiddleButton":
                    ResizeRightMiddle(delta, ref vm);
                    break;
                case "TopMiddleButton":
                    ResizeTopMiddle(delta, ref vm);
                    break;
                case "BottomMiddleButton":
                    ResizeBottomMiddle(delta, ref vm);
                    break;
            }

            UpdateButtonPositions(vm.SelectionX, vm.SelectionY, vm.SelectionWidth, vm.SelectionHeight);
            UpdateSurroundingRectangles();
        }
    }
    
    private void UpdateButtonPositions(double selectionX, double selectionY, double selectionWidth, double selectionHeight)
    {
        try
        {
            // Top Left Button
            Canvas.SetLeft(TopLeftButton, Convert.ToInt32(selectionX - TopLeftButton.Width / 2));
            Canvas.SetTop(TopLeftButton, Convert.ToInt32(selectionY - TopLeftButton.Height / 2));

            // Top Right Button
            Canvas.SetLeft(TopRightButton, Convert.ToInt32(selectionX + selectionWidth - TopRightButton.Width / 2));
            Canvas.SetTop(TopRightButton, Convert.ToInt32(selectionY - TopRightButton.Height / 2));

            // Top Middle Button
            Canvas.SetLeft(TopMiddleButton, Convert.ToInt32(selectionX + (selectionWidth / 2) - TopMiddleButton.Width / 2));
            Canvas.SetTop(TopMiddleButton, Convert.ToInt32(selectionY - TopMiddleButton.Height / 2));

            // Bottom Left Button
            Canvas.SetLeft(BottomLeftButton, Convert.ToInt32(selectionX - BottomLeftButton.Width / 2));
            Canvas.SetTop(BottomLeftButton, Convert.ToInt32(selectionY + selectionHeight - BottomLeftButton.Height / 2));

            // Bottom Right Button
            Canvas.SetLeft(BottomRightButton, Convert.ToInt32(selectionX + selectionWidth - BottomRightButton.Width / 2));
            Canvas.SetTop(BottomRightButton, Convert.ToInt32(selectionY + selectionHeight - BottomRightButton.Height / 2));

            // Bottom Middle Button
            Canvas.SetLeft(BottomMiddleButton, Convert.ToInt32(selectionX + (selectionWidth / 2) - BottomMiddleButton.Width / 2));
            Canvas.SetTop(BottomMiddleButton, Convert.ToInt32(selectionY + selectionHeight - BottomMiddleButton.Height / 2));

            // Left Middle Button
            Canvas.SetLeft(LeftMiddleButton, Convert.ToInt32(selectionX - LeftMiddleButton.Width / 2));
            Canvas.SetTop(LeftMiddleButton, Convert.ToInt32(selectionY + (selectionHeight / 2) - LeftMiddleButton.Height / 2));

            // Right Middle Button
            Canvas.SetLeft(RightMiddleButton, Convert.ToInt32(selectionX + selectionWidth - RightMiddleButton.Width / 2));
            Canvas.SetTop(RightMiddleButton, Convert.ToInt32(selectionY + (selectionHeight / 2) - RightMiddleButton.Height / 2));
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
    
    private void ResizeTopLeft(Vector delta, ref ImageCropperViewModel vm)
    {
        if (vm.SelectionX is 0 && vm.SelectionY is 0)
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
            _isDragging = false;
            _isResizing = false;
            return;
        }
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

        // Apply the new size and position
        vm.SelectionX = newLeft;
        vm.SelectionY = newTop;
        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        Canvas.SetLeft(MainRectangle, newLeft);
        Canvas.SetTop(MainRectangle, newTop);
    }

    private void ResizeTopRight(Vector delta, ref ImageCropperViewModel vm)
    {
        var newWidth = _originalRect.Width + delta.X;
        var newHeight = _originalRect.Height - delta.Y;
        var newY = _originalRect.Y + delta.Y;

        if (!(newWidth > 0) || !(newHeight > 0))
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
            _isDragging = false;
            _isResizing = false;
            return;
        }

        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        vm.SelectionY = newY;
        Canvas.SetTop(MainRectangle, newY);
    }

    private void ResizeBottomLeft(Vector delta, ref ImageCropperViewModel vm)
    {
        var newWidth = _originalRect.Width - delta.X;
        var newHeight = _originalRect.Height + delta.Y;
        var newX = _originalRect.X + delta.X;

        if (newWidth < 0 || newHeight < 0 || newX < 0)
        {
            Cursor = new Cursor(StandardCursorType.Arrow);
            _isDragging = false;
            _isResizing = false;
            return;
        }

        newWidth = Math.Max(newWidth, 7);
        newHeight = Math.Max(newHeight, 7);

        vm.SelectionWidth = newWidth;
        vm.SelectionHeight = newHeight;
        vm.SelectionX = newX;
        Canvas.SetLeft(MainRectangle, newX);
        
        TooltipHelper.ShowTooltipMessage($"Width: {vm.SelectionWidth}, Height: {vm.SelectionHeight}, x: {vm.SelectionX}, y: {vm.SelectionY}");
    }

    private void ResizeBottomRight(Vector delta, ref ImageCropperViewModel vm)
    {
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
    }

    private void ResizeLeftMiddle(Vector delta, ref ImageCropperViewModel vm)
    {
        var newWidth = _originalRect.Width - delta.X;
        var newX = _originalRect.X + delta.X;

        if (!(newWidth > 0))
        {
            return;
        }

        vm.SelectionWidth = newWidth;
        vm.SelectionX = newX;
        Canvas.SetLeft(MainRectangle, newX);
    }

    private void ResizeRightMiddle(Vector delta, ref ImageCropperViewModel vm)
    {
        var newWidth = _originalRect.Width + delta.X;

        if (newWidth > 0)
        {
            vm.SelectionWidth = newWidth;
        }
    }

    private void ResizeTopMiddle(Vector delta, ref ImageCropperViewModel vm)
    {
        var newHeight = _originalRect.Height - delta.Y;
        var newY = _originalRect.Y + delta.Y;

        vm.SelectionHeight = newHeight < 0 ? 0 : newHeight;
        vm.SelectionY = newY < 0 ? 0 : newY;
        Canvas.SetTop(MainRectangle, newY);
    }

    private void ResizeBottomMiddle(Vector delta, ref ImageCropperViewModel vm)
    {
        var newHeight = _originalRect.Height + delta.Y;

        if (newHeight > 0)
        {
            vm.SelectionHeight = newHeight;
        }
    }
    
    private void OnResizePointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _isResizing = false;
    }
}