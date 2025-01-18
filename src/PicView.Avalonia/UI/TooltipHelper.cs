using Avalonia;
using Avalonia.Threading;
using PicView.Avalonia.Animations;
using VerticalAlignment = Avalonia.Layout.VerticalAlignment;

namespace PicView.Avalonia.UI;

/// <summary>
/// Provides helper methods for displaying the tooltip in the application.
/// </summary>
public static class TooltipHelper
{
    private static bool _isRunning;
    
    private static CancellationTokenSource? _cancellationTokenSource;

    /// <summary>
    /// Shows the tooltip message on the UI.
    /// </summary>
    /// <param name="message">The message to display in the tooltip.</param>
    /// <param name="center">Determines whether the tooltip should be centered or aligned at the bottom.</param>
    /// <param name="interval">The duration for which the tooltip should be visible.</param>
    public static async Task ShowTooltipMessageAsync(object message, bool center, TimeSpan interval)
    {
        try
        {
            var endAnimation = AnimationsHelper.OpacityAnimation(1, 0, .5);

            // ReSharper disable once MethodHasAsyncOverload
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            
            await Dispatcher.UIThread.InvokeAsync(() =>
            {
                var toolTip = UIHelper.GetToolTipMessage;
                toolTip.ToolTipMessageText.Text = message.ToString();
                UIHelper.GetToolTipMessage.IsVisible = true;
                
                if (!_isRunning)
                {
                    UIHelper.GetToolTipMessage.Margin = center ? new Thickness(0) : new Thickness(0, 0, 0, 15);
                    UIHelper.GetToolTipMessage.VerticalAlignment =
                        center ? VerticalAlignment.Center : VerticalAlignment.Bottom;
                }
                else
                {
                    toolTip.Opacity = 1;
                }
            }, DispatcherPriority.Normal, _cancellationTokenSource.Token);

            if (!_isRunning)
            {
                _isRunning = true;
                await Dispatcher.UIThread.InvokeAsync(() =>
                {
                    UIHelper.GetToolTipMessage.Opacity = 1;
                }, DispatcherPriority.Normal, _cancellationTokenSource.Token);
                await Task.Delay(interval, _cancellationTokenSource.Token);
                await endAnimation.RunAsync(UIHelper.GetToolTipMessage, _cancellationTokenSource.Token);
            }
        }
        catch (TaskCanceledException)
        {
            // ignored
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine($"{nameof(ShowTooltipMessageAsync)} exception {e.Message}: \n{e.StackTrace}");
#endif
        }
        finally
        {
            _isRunning = false;
        }
    }

    /// <summary>
    /// Shows the tooltip message on the UI.
    /// </summary>
    /// <param name="message">The message to display in the tooltip.</param>
    /// <param name="center">Determines whether the tooltip should be centered or aligned at the bottom.</param>
    /// <param name="interval">The duration for which the tooltip should be visible.</param>
    public static void ShowTooltipMessage(object message, bool center, TimeSpan interval)
    {
        var timer = new DispatcherTimer { Interval = interval };
        timer.Tick += (_, _) =>
        {
            if (!_isRunning)
            {
                Dispatcher.UIThread.Invoke(() =>
                {
                    var toolTip = UIHelper.GetToolTipMessage;
                    if (toolTip != null)
                    {
                        toolTip.Opacity = 0;
                    }
                });
            }

            _isRunning = false;
            timer.Stop();
        };
        timer.Start();

        try
        {
            Dispatcher.UIThread.Invoke(() =>
            {
                var toolTip = UIHelper.GetToolTipMessage;
                if (toolTip is null)
                {
                    return;
                }

                _isRunning = true;
                toolTip.IsVisible = true;
                toolTip.ToolTipMessageText.Text = message.ToString();
                toolTip.Margin = center ? new Thickness(0) : new Thickness(0, 0, 0, 15);
                toolTip.VerticalAlignment = center ? VerticalAlignment.Center : VerticalAlignment.Bottom;
                toolTip.Opacity = 1;
            });
        }
        catch (Exception exception)
        {
#if DEBUG
            Console.WriteLine($"{nameof(ShowTooltipMessage)} exception, \n{exception.Message}");
#endif
        }
    }

    /// <summary>
    /// Shows the tooltip message on the UI with a default duration of 2 seconds.
    /// </summary>
    /// <param name="message">The message to display in the tooltip.</param>
    /// <param name="center">Determines whether the tooltip should be centered or aligned at the bottom.</param>
    internal static void ShowTooltipMessage(object message, bool center = false)
    {
        ShowTooltipMessage(message, center, TimeSpan.FromSeconds(2));
    }

    /// <summary>
    /// Shows the tooltip message on the UI with a default duration of 2 seconds.
    /// </summary>
    /// <param name="message">The message to display in the tooltip.</param>
    /// <param name="center">Determines whether the tooltip should be centered or aligned at the bottom.</param>
    internal static async Task ShowTooltipMessageAsync(object message, bool center = false)
    {
        await ShowTooltipMessageAsync(message, center, TimeSpan.FromSeconds(2));
    }
}