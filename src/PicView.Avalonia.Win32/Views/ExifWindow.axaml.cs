using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using PicView.Core.Config;
using PicView.Core.Localization;

namespace PicView.Avalonia.Win32.Views;

public partial class ExifWindow : Window
{
    public ExifWindow()
    {
        InitializeComponent();
        if (SettingsHelper.Settings.Theme.GlassTheme)
        {
            BorderRectangle.Height = 0;
            
            TopWindowBorder.Background = Brushes.Transparent;
            StarOutlineButtons.Background = Brushes.Transparent;
            RemoveRatingButton.Background = Brushes.Transparent;
            
            CloseButton.Background = Brushes.Transparent;
            CloseButton.BorderThickness = new Thickness(0);
            MinimizeButton.Background = Brushes.Transparent;
            MinimizeButton.BorderThickness = new Thickness(0);
            
            RecycleButton.BorderThickness = new Thickness(0);
            DuplicateButton.BorderThickness = new Thickness(0);
            OptimizeButton.BorderThickness = new Thickness(0);
            OpenWithButton.BorderThickness = new Thickness(0);
            LocateOnDiskButton.BorderThickness = new Thickness(0);
            
            if (!Application.Current.TryGetResource("SecondaryTextColor",
                    Application.Current.RequestedThemeVariant, out var textColor))
            {
                return;
            }

            if (textColor is not Color color)
            {
                return;
            }
            
            MinimizeButton.Foreground = new SolidColorBrush(color);
            CloseButton.Foreground = new SolidColorBrush(color);
            RecycleButton.Foreground = new SolidColorBrush(color);
            DuplicateButton.Foreground = new SolidColorBrush(color);
            OptimizeButton.Foreground = new SolidColorBrush(color);
            OpenWithButton.Foreground = new SolidColorBrush(color);
            LocateOnDiskButton.Foreground = new SolidColorBrush(color);
        }
        else if (!SettingsHelper.Settings.Theme.Dark)
        {
            ParentBorder.Background = new SolidColorBrush(Color.FromArgb(114,132, 132, 132));
        }

        if (SettingsHelper.Settings.Theme.GlassTheme || !SettingsHelper.Settings.Theme.Dark)
        {
            RecycleButton.Classes.Remove("noBorderHover");
            RecycleButton.Classes.Add("hover");
            
            DuplicateButton.Classes.Remove("noBorderHover");
            DuplicateButton.Classes.Add("hover");
            
            CopyButton.Classes.Remove("noBorderHover");
            CopyButton.Classes.Add("hover");
            
            CopyFileButton.Classes.Remove("noBorderHover");
            CopyFileButton.Classes.Add("hover");
            
            PrintButton.Classes.Remove("noBorderHover");
            PrintButton.Classes.Add("hover");
            
            OptimizeButton.Classes.Remove("noBorderHover");
            OptimizeButton.Classes.Add("hover");
            
            OpenWithButton.Classes.Remove("noBorderHover");
            OpenWithButton.Classes.Add("hover");
            
            LocateOnDiskButton.Classes.Remove("noBorderHover");
            LocateOnDiskButton.Classes.Add("hover");
            
            RemoveRatingButton.Classes.Remove("noBorderHover");
            RemoveRatingButton.Classes.Add("hover");
        }
        Title = TranslationHelper.GetTranslation("ImageInfo") + " - PicView";
        KeyDown += (_, e) =>
        {
            if (e.Key is Key.Escape)
            {
                Close();
            }
        };
    }

    private void MoveWindow(object? sender, PointerPressedEventArgs e)
    {
        if (VisualRoot is null) { return; }

        var hostWindow = (Window)VisualRoot;
        hostWindow?.BeginMoveDrag(e);
    }

    private void Close(object? sender, RoutedEventArgs e)
    {
        Close();
    }

    private void Minimize(object? sender, RoutedEventArgs e)
    {
        WindowState = WindowState.Minimized;
    }
}