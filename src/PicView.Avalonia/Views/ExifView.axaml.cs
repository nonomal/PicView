using System.Globalization;
using System.Reactive.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using PicView.Avalonia.Converters;
using PicView.Avalonia.FileSystem;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.Resizing;
using PicView.Avalonia.ViewModels;
using PicView.Core.ImageDecoding;
using ReactiveUI;

namespace PicView.Avalonia.Views;

public partial class ExifView : UserControl
{
    private IDisposable? _imageUpdateSubscription;
    
    public ExifView()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            KeyDown += (_, e) =>
            {
                switch (e.Key)
                {
                    case Key.Down:
                    case Key.PageDown:
                        ScrollViewer.LineDown();
                        break;
                    case Key.Up:
                    case Key.PageUp:
                        ScrollViewer.LineUp();
                        break;
                    case Key.Home:
                        ScrollViewer.ScrollToHome();
                        break;
                    case Key.End:
                        ScrollViewer.ScrollToEnd();
                        break;
                }
            };
            
            PixelWidthTextBox.KeyDown += async (s, e) => await OnKeyDownVerifyInput(s,e);
            PixelHeightTextBox.KeyDown += async (s, e) => await OnKeyDownVerifyInput(s,e);

            PixelWidthTextBox.KeyUp += delegate { AdjustAspectRatio(PixelWidthTextBox); };
            PixelHeightTextBox.KeyUp += delegate { AdjustAspectRatio(PixelHeightTextBox); };
            
            if (DataContext is not MainViewModel vm)
            {
                return;
            }
            
            ExifHandling.UpdateExifValues(vm);
            
            _imageUpdateSubscription = vm.WhenAnyValue(x => x.FileInfo).Select(x => x is not null).Subscribe(_ =>
            {
                ExifHandling.UpdateExifValues(vm);
            });
            ResetButton.Click += (_, _) =>
            {
                PixelWidthTextBox.Text = vm.PixelWidth.ToString();
                PixelHeightTextBox.Text = vm.PixelHeight.ToString();
                AdjustAspectRatio(PixelWidthTextBox);
                FullPathTextBox.Text = vm.FileInfo?.FullName ?? "";
                DirectoryNameTextBox.Text = vm.FileInfo?.DirectoryName ?? "";
                FileNameTextBox.Text = vm.FileInfo?.Name ?? ""; 
            };
            
            SaveButton.Click += async (_, _) =>
            {
                var ext = GetExtension();
                var location = FullPathTextBox.Text; // TODO check if this is a valid path
                                                     // and sync with file name/directory text boxes
                await SendToImageSaver(vm.FileInfo?.FullName, location, PixelWidthTextBox.Text, PixelHeightTextBox.Text, ext).ConfigureAwait(false);
            };

            SaveAsButton.Click += async (_, _) =>
            {
                var file = await FilePicker.PickFileForSavingAsync(vm.FileInfo?.FullName);
                if (file is null)
                {
                    return;
                }
                var ext = GetExtension();
                await SendToImageSaver( vm.FileInfo?.FullName, file, PixelWidthTextBox.Text, PixelHeightTextBox.Text, ext).ConfigureAwait(false);
            };
        };
    }

    private async Task SendToImageSaver(string? location, string destination, string? width, string? height, string ext)
    {
        if (!uint.TryParse(width, out var widthValue) || !uint.TryParse(height, out var heightValue))
        {
            return;
        }
        await SaveImageFileHelper.SaveImageAsync(null, location, destination, widthValue, heightValue, null, ext).ConfigureAwait(false);
    }

    private string GetExtension () => ConversionComboBox.SelectedIndex switch
    {
        1 => ".png",
        2 => ".jpg",
        3 => ".webp",
        4 => ".avif",
        5 => ".heic",
        6 => ".jxl",
        _ => ""
    };

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _imageUpdateSubscription.Dispose();
    }

    private void AdjustAspectRatio(TextBox sender)
    {
        if (DataContext is not MainViewModel vm)
        {
            return;
        }
        var aspectRatio = (double)vm.PixelWidth / vm.PixelHeight;
        AspectRatioHelper.SetAspectRatioForTextBox(PixelWidthTextBox, PixelHeightTextBox, sender == PixelWidthTextBox,
            aspectRatio, DataContext as MainViewModel);
        AspectRatioTextBox.Text = aspectRatio.ToString(CultureInfo.CurrentCulture);
        if (!int.TryParse(PixelWidthTextBox.Text, out var width) || !int.TryParse(PixelHeightTextBox.Text, out var height))
        {
            return;
        }
        var printSizes = AspectRatioHelper.GetPrintSizes(width, height, vm.DpiX, vm.DpiY);
        PrintSizeInchTextBox.Text = printSizes.PrintSizeInch;
        PrintSizeCmTextBox.Text = printSizes.PrintSizeCm;
        SizeMpTextBox.Text = printSizes.SizeMp;
    }

    private static async Task DoResize(MainViewModel vm, bool isWidth, object width, object height)
    {
        if (isWidth)
        {
            if (!double.TryParse((string?)width, out var widthValue))
            {
                return;
            }
            if (widthValue > 0)
            {
                var success = await ConversionHelper.ResizeByWidth(vm.FileInfo, widthValue).ConfigureAwait(false);
                if (success)
                {
                    if (vm.ImageIterator is not null)
                    {
                        await vm.ImageIterator.QuickReload().ConfigureAwait(false);
                    }
                }
            }
        }
        else
        {
            if (!double.TryParse((string?)height, out var heightValue))
            {
                return;
            }
            if (heightValue > 0)
            {
                var success = await ConversionHelper.ResizeByHeight(vm.FileInfo, heightValue).ConfigureAwait(false);
                if (success)
                {
                    vm.ImageIterator?.RemoveCurrentItemFromPreLoader();
                    await vm.ImageIterator?.IterateToIndex(vm.ImageIterator.CurrentIndex);
                }
            }
        }
    }
    
    private async Task OnKeyDownVerifyInput(object? sender, KeyEventArgs? e)
    {
        switch (e.Key)
        {
            case Key.D0:
            case Key.D1:
            case Key.D2:
            case Key.D3:
            case Key.D4:
            case Key.D5:
            case Key.D6:
            case Key.D7:
            case Key.D8:
            case Key.D9:
            case Key.NumPad0:
            case Key.NumPad1:
            case Key.NumPad2:
            case Key.NumPad3:
            case Key.NumPad4:
            case Key.NumPad5:
            case Key.NumPad6:
            case Key.NumPad7:
            case Key.NumPad8:
            case Key.NumPad9:
            case Key.Back:
            case Key.Delete:
                break; // Allow numbers and basic operations
    
            case Key.Left:
            case Key.Right:
            case Key.Tab:
            case Key.OemBackTab:
                break; // Allow navigation keys
    
            case Key.A:
            case Key.C:
            case Key.X:
            case Key.V:
                if (e.KeyModifiers == KeyModifiers.Control)
                {
                    // Allow Ctrl + A, Ctrl + C, Ctrl + X, and Ctrl + V (paste)
                    break;
                }
    
                e.Handled = true; // Only allow with Ctrl
                return;
    
            case Key.Oem5: // Key for `%` symbol (may vary based on layout)
                break; // Allow the percentage symbol (%)
    
            case Key.Escape: // Handle Escape key
                Focus();
                e.Handled = true;
                return;
    
            case Key.Enter: // Handle Enter key for saving
                if (DataContext is not MainViewModel vm)
                {
                    return;
                }

                await DoResize(vm, Equals(sender, PixelWidthTextBox), PixelWidthTextBox.Text, PixelHeightTextBox.Text).ConfigureAwait(false);
                return;
    
            default:
                e.Handled = true; // Block all other inputs
                return;
        }
    }
}