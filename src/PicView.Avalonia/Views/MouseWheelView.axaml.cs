using Avalonia.Controls;

namespace PicView.Avalonia.Views;
    public partial class MouseWheelView : UserControl
    {
        public MouseWheelView()
        {
            InitializeComponent();
            Loaded += delegate
            {
                MouseWheelBox.SelectedIndex = Settings.Zoom.CtrlZoom ? 0 : 1;

                MouseWheelBox.SelectionChanged += async delegate
                {
                    if (MouseWheelBox.SelectedIndex == -1)
                    {
                        return;
                    }

                    Settings.Zoom.CtrlZoom = MouseWheelBox.SelectedIndex == 0;
                    await SaveSettingsAsync();
                };
                MouseWheelBox.DropDownOpened += delegate
                {
                    if (MouseWheelBox.SelectedIndex == -1)
                    {
                        MouseWheelBox.SelectedIndex = Settings.Zoom.CtrlZoom ? 0 : 1;
                    }
                };
            };
            
            ScrollDirectionBox.SelectedIndex = Settings.Zoom.HorizontalReverseScroll ? 0 : 1;
            
            ScrollDirectionBox.SelectionChanged += async delegate
            {
                if (ScrollDirectionBox.SelectedIndex == -1)
                {
                    return;
                }
                Settings.Zoom.HorizontalReverseScroll = ScrollDirectionBox.SelectedIndex == 0;
                await SaveSettingsAsync();
            };
            ScrollDirectionBox.DropDownOpened += delegate
            {
                if (ScrollDirectionBox.SelectedIndex == -1)
                {
                    ScrollDirectionBox.SelectedIndex = Settings.Zoom.HorizontalReverseScroll ? 0 : 1;
                }
            };
        }
    }
