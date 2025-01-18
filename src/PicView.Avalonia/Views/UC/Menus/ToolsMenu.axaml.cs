using Avalonia.Media;
using PicView.Avalonia.CustomControls;
using PicView.Core.Config;

namespace PicView.Avalonia.Views.UC.Menus;

public partial class ToolsMenu : AnimatedMenu
{
    public ToolsMenu()
    {
        InitializeComponent();
        Loaded += (_, _) =>
        {
            if (SettingsHelper.Settings.Theme.GlassTheme)
            {
                BatchResizeButton.Classes.Remove("noBorderHover");
                BatchResizeButton.Classes.Add("hover");
                
                EffectsButton.Classes.Remove("noBorderHover");
                EffectsButton.Classes.Add("hover");
            }
            else if (!SettingsHelper.Settings.Theme.Dark)
            {
                TopBorder.Background = Brushes.White;
            }
        };
    }
}