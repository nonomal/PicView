using Avalonia.Controls;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Core.Config;

namespace PicView.Avalonia.Views.UC.Buttons;
public partial class ClickArrowRight : UserControl
{
    public ClickArrowRight()
    {
        InitializeComponent();
        Loaded += delegate
        {
            if (DataContext is not MainViewModel vm)
            {
                return;
            }
            HideInterfaceLogic.AddHoverButtonEvents(this, PolyButton, vm);
            PointerWheelChanged += async (_, e) => await vm.ImageViewer.PreviewOnPointerWheelChanged(this, e);
            
            // TODO add interval to mainviewmodel
            PolyButton.Interval =
                (int)TimeSpan.FromSeconds(SettingsHelper.Settings.UIProperties.NavSpeed).TotalMilliseconds;
        };
    }
}
