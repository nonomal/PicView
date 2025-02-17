using Avalonia.Media;
using PicView.Avalonia.CustomControls;

namespace PicView.Avalonia.Views.UC.Menus;

public partial class ToolsMenu : AnimatedMenu
{
    public ToolsMenu()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            if (Settings.Theme.GlassTheme)
            {
                BatchResizeButton.Classes.Remove("noBorderHover");
                BatchResizeButton.Classes.Add("hover");
                
                EffectsButton.Classes.Remove("noBorderHover");
                EffectsButton.Classes.Add("hover");
            }
            else if (!Settings.Theme.Dark)
            {
                TopBorder.Background = Brushes.White;
            }
        };
    }
}