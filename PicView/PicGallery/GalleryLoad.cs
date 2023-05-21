﻿using PicView.ChangeImage;
using PicView.ImageHandling;
using PicView.Properties;
using PicView.UILogic;
using PicView.UILogic.Sizing;
using PicView.Views.UserControls.Gallery;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using static PicView.UILogic.HideInterfaceLogic;

namespace PicView.PicGallery;

internal static class GalleryLoad
{
    internal static bool IsLoading { get; private set; }

    internal const int FullscreenItems = 37;
    internal const int GalleryItems = 23;

    internal static void PicGallery_Loaded(object sender, RoutedEventArgs e)
    {
        // Add events and set fields, when it's loaded.
        UC.GetPicGallery.Scroller.PreviewMouseWheel += (_, e) => GalleryNavigation.ScrollTo(e.Delta > 0);
        UC.GetPicGallery.Scroller.ScrollChanged += (_, _) => ConfigureWindows.GetMainWindow.Focus(); // Maintain window focus when scrolling manually
        UC.GetPicGallery.grid.MouseLeftButtonDown += (_, _) => ConfigureWindows.GetMainWindow.Focus();
        UC.GetPicGallery.x2.MouseLeftButtonDown += (_, _) => GalleryToggle.CloseHorizontalGallery();
    }

    internal static void LoadLayout()
    {
        if (UC.GetPicGallery == null)
        {
            UC.GetPicGallery = new Views.UserControls.Gallery.PicGallery
            {
                Opacity = 0
            };

            ConfigureWindows.GetMainWindow.ParentContainer.Children.Add(UC.GetPicGallery);
            Panel.SetZIndex(UC.GetPicGallery, 999);
        }

        GalleryFunctions.IsGalleryOpen = true;

        if (Settings.Default.FullscreenGallery)
        {
            WindowSizing.RenderFullscreen();

            // Set size
            GalleryNavigation.SetSize(FullscreenItems);
            UC.GetPicGallery.Width = WindowSizing.MonitorInfo.WorkArea.Width;
            UC.GetPicGallery.Height = double.NaN;

            // Set alignment
            UC.GetPicGallery.HorizontalAlignment = HorizontalAlignment.Center;
            UC.GetPicGallery.VerticalAlignment = VerticalAlignment.Bottom;

            // Set scrollbar visibility and orientation
            UC.GetPicGallery.Scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            UC.GetPicGallery.Scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            UC.GetPicGallery.Container.Orientation = Orientation.Horizontal;

            // Set style
            UC.GetPicGallery.x2.Visibility = Visibility.Collapsed;
            UC.GetPicGallery.Container.Margin = new Thickness(0, 0, 0, 4 * WindowSizing.MonitorInfo.DpiScaling);
            UC.GetPicGallery.border.BorderThickness = new Thickness(1);
            UC.GetPicGallery.Container.MinHeight = GalleryNavigation.PicGalleryItemSize;

            ShowNavigation(false);
            ShowTopAndBottom(false);
            ConfigureWindows.GetMainWindow.Focus();
        }
        else
        {
            // Make sure booleans are correct
            GalleryFunctions.IsGalleryOpen = true;

            // Set size
            GalleryNavigation.SetSize(GalleryItems);
            UC.GetPicGallery.Width = ConfigureWindows.GetMainWindow.ParentContainer.ActualWidth;
            UC.GetPicGallery.Height = ConfigureWindows.GetMainWindow.ParentContainer.ActualHeight;

            // Set alignment
            UC.GetPicGallery.HorizontalAlignment = HorizontalAlignment.Stretch;
            UC.GetPicGallery.VerticalAlignment = VerticalAlignment.Stretch;

            // Set scrollbar visibility and orientation
            UC.GetPicGallery.Scroller.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
            UC.GetPicGallery.Scroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
            UC.GetPicGallery.Container.Orientation = Orientation.Vertical;

            // Set style
            UC.GetPicGallery.x2.Visibility = Visibility.Visible;
            UC.GetPicGallery.Container.Margin = new Thickness(0, 60 * WindowSizing.MonitorInfo.DpiScaling, 0, 0);
            UC.GetPicGallery.border.BorderThickness = new Thickness(1, 0, 0, 0);
            UC.GetPicGallery.border.Background = (SolidColorBrush)Application.Current.Resources["BackgroundColorBrushFade"];
        }

        if (UC.GetPicGallery.Container.Children.Count <= 0) { return; }
        var tempItem = (PicGalleryItem)UC.GetPicGallery.Container.Children[0];

        if (Math.Abs(tempItem.OuterBorder.Height - GalleryNavigation.PicGalleryItemSize) < 1) { return; }

        for (int i = 0; i < UC.GetPicGallery.Container.Children.Count; i++)
        {
            var item = (PicGalleryItem)UC.GetPicGallery.Container.Children[i];
            item.InnerBorder.Height = item.InnerBorder.Width = GalleryNavigation.PicGalleryItemSizeS;
            item.OuterBorder.Height = item.OuterBorder.Width = GalleryNavigation.PicGalleryItemSize;
        }
    }

    internal static async Task LoadAsync()
    {
        IsLoading = true;
        var source = new CancellationTokenSource();
        try
        {
            await LoopAsync(source.Token).ConfigureAwait(false);
        }
        catch (TaskCanceledException)
        {
            ConfigureWindows.GetMainWindow.Dispatcher.Invoke(DispatcherPriority.Background,
                new Action(() => { UC.GetPicGallery.Container.Children.Clear(); }));
            await LoadAsync().ConfigureAwait(false);
        }
        finally
        {
            IsLoading = false;
            source.Dispose();
        }
    }

    private static async Task LoopAsync(CancellationToken cancellationToken)
    {
        if (UC.GetPicGallery is null) { return; }

        var count = Navigation.Pics.Count;
        var index = Navigation.FolderIndex;

        for (int i = 0; i < count; i++)
        {
            if (count != Navigation.Pics.Count)
            {
                throw new TaskCanceledException();
            }

            Add(i, index);
        }

        await Parallel.ForEachAsync(Navigation.Pics, cancellationToken, async (item, token) =>
        {
            if (count != Navigation.Pics.Count)
            {
                throw new TaskCanceledException();
            }
            if (cancellationToken.IsCancellationRequested)
            {
                cancellationToken.ThrowIfCancellationRequested();
            }
            await UpdatePic(item).ConfigureAwait(false);
        });
    }

    internal static void Add(int i, int index)
    {
        ConfigureWindows.GetMainWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
        {
            var selected = i == index;
            var item = new PicGalleryItem(null, i, selected);
            item.MouseLeftButtonDown += async delegate
            {
                await GalleryClick.ClickAsync(i).ConfigureAwait(false);
            };

            UC.GetPicGallery.Container.Children.Add(item);

            if (!selected) { return; }
            GalleryNavigation.SelectedGalleryItem = i;
            Timers.PicGalleryTimerHack();
        }));
    }

    private static async Task UpdatePic(string file)
    {
        if (Navigation.Pics?.Count < Navigation.FolderIndex || Navigation.Pics?.Count < 1)
        {
            GalleryFunctions.Clear();
            await LoadAsync().ConfigureAwait(false); // restart when changing directory
            return;
        }

        var source = await Task.FromResult(Thumbnails.GetBitmapSourceThumb(new FileInfo(file), (int)GalleryNavigation.PicGalleryItemSize));
        UpdatePic(Navigation.Pics.IndexOf(file), source);
    }

    internal static void UpdatePic(int i, BitmapSource? pic)
    {
        try
        {
            ConfigureWindows.GetMainWindow.Dispatcher.Invoke(DispatcherPriority.Normal, new Action(() =>
            {
                if (Navigation.Pics?.Count < Navigation.FolderIndex || Navigation.Pics?.Count < 1 || i >= UC.GetPicGallery.Container.Children.Count)
                {
                    GalleryFunctions.Clear();
                    LoadAsync().ConfigureAwait(false); // restart when changing directory
                    return;
                }

                var item = (PicGalleryItem)UC.GetPicGallery.Container.Children[i];
                item.ThumbImage.Source = pic ?? ImageFunctions.ShowLogo();
            }));
        }
        catch (Exception e)
        {
#if DEBUG
            Trace.WriteLine(e.Message);
#endif
            // Suppress task cancellation
        }
    }
}