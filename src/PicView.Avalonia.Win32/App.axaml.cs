﻿using System.Runtime;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PicView.Avalonia.Interfaces;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.Win32.Views;
using PicView.Core.Config;
using PicView.Core.FileHandling;
using PicView.Core.Localization;
using PicView.Core.ProcessHandling;
using PicView.Windows;
using PicView.Windows.FileHandling;
using PicView.Windows.Lockscreen;
using PicView.Windows.Taskbar;
using PicView.Windows.Wallpaper;
using Dispatcher = Avalonia.Threading.Dispatcher;

namespace PicView.Avalonia.Win32;

public class App : Application, IPlatformSpecificService
{
    private WinMainWindow? _mainWindow;
    private ExifWindow? _exifWindow;
    private SettingsWindow? _settingsWindow;
    private KeybindingsWindow? _keybindingsWindow;
    private AboutWindow? _aboutWindow;
    private MainViewModel? _vm;
    
    private TaskbarProgress? _taskbarProgress;

    public override void Initialize()
    {
        ProfileOptimization.SetProfileRoot(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/"));
        ProfileOptimization.StartProfile("ProfileOptimization");
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        base.OnFrameworkInitializationCompleted();

        if (ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
        {
            return;
        }

        bool settingsExists;
        try
        {
            settingsExists = await SettingsHelper.LoadSettingsAsync().ConfigureAwait(false);
            await TranslationHelper.LoadLanguage(SettingsHelper.Settings.UIProperties.UserLanguage);
        }
        catch (TaskCanceledException)
        {
            return;
        }

        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainWindow = new WinMainWindow();
            desktop.MainWindow = _mainWindow;
        });
        _vm = new MainViewModel(this);
        await Dispatcher.UIThread.InvokeAsync(() =>
        {
            _mainWindow.DataContext = _vm;
            StartUpHelper.Start(_vm, settingsExists, desktop, _mainWindow);
        });
    }
    
    #region Interface Implementations
    
    public void SetTaskbarProgress(ulong progress, ulong maximum)
    {
        if (_taskbarProgress is null)
        {
            var handle = _mainWindow?.TryGetPlatformHandle()?.Handle;
    
            // Ensure the handle is valid before proceeding
            if (handle == IntPtr.Zero || handle is null)
            {
                return;
            }
            _taskbarProgress = new TaskbarProgress(handle.Value);
        }

        _taskbarProgress.SetProgress(progress, maximum);
    }
    
    public void StopTaskbarProgress()
    {
        var handle = _mainWindow?.TryGetPlatformHandle()?.Handle;
    
        // Ensure the handle is valid before proceeding
        if (handle == IntPtr.Zero || handle is null)
        {
            return;
        }
        _taskbarProgress?.StopProgress();
    }

    public void SetCursorPos(int x, int y)
    {
        NativeMethods.SetCursorPos(x, y);
    }

    public List<string> GetFiles(FileInfo fileInfo)
    {
        var files = FileListHelper.RetrieveFiles(fileInfo);
        return SortingHelper.SortIEnumerable(files, this);
    }

    public int CompareStrings(string str1, string str2)
    {
        return NativeMethods.StrCmpLogicalW(str1, str2);
    }

    public void OpenWith(string path)
    {
        ProcessHelper.OpenWith(path);
    }

    public void LocateOnDisk(string path)
    {
        var folder = Path.GetDirectoryName(path);
        FileExplorer.OpenFolderAndSelectFile(folder, path);
    }

    public void ShowFileProperties(string path)
    {
        FileExplorer.ShowFileProperties(path);
    }

    public void ShowAboutWindow()
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            Set();
        }
        else
        {
            Dispatcher.UIThread.InvokeAsync(Set);
        }
        return;

        void Set()
        {
            if (Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }

            if (_aboutWindow is null)
            {
                _aboutWindow = new AboutWindow
                {
                    DataContext = _vm,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                _aboutWindow.Show(desktop.MainWindow);
                _aboutWindow.Closing += (s, e) => _aboutWindow = null;
            }
            else
            {
                _aboutWindow.Activate();
            }

            _ = FunctionsHelper.CloseMenus();
        }
    }

    public void ShowExifWindow()
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            Set();
        }
        else
        {
            Dispatcher.UIThread.InvokeAsync(Set);
        }
        return;

        void Set()
        {
            if (Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }

            if (_exifWindow is null)
            {
                _exifWindow = new ExifWindow
                {
                    DataContext = _vm,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                _exifWindow.Show(desktop.MainWindow);
                _exifWindow.Closing += (s, e) => _exifWindow = null;
            }
            else
            {
                _exifWindow.Activate();
            }

            _ = FunctionsHelper.CloseMenus();
        }
    }

    public void ShowKeybindingsWindow()
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            Set();
        }
        else
        {
            Dispatcher.UIThread.InvokeAsync(Set);
        }
        return;

        void Set()
        {
            if (Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }

            if (_keybindingsWindow is null)
            {
                _keybindingsWindow = new KeybindingsWindow
                {
                    DataContext = _vm,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                _keybindingsWindow.Show(desktop.MainWindow);
                _keybindingsWindow.Closing += (s, e) => _keybindingsWindow = null;
            }
            else
            {
                _keybindingsWindow.Activate();
            }

            _ = FunctionsHelper.CloseMenus();
        }
    }

    public void ShowSettingsWindow()
    {
        if (Dispatcher.UIThread.CheckAccess())
        {
            Set();
        }
        else
        {
            Dispatcher.UIThread.InvokeAsync(Set);
        }
        return;
        void Set()
        {
            if (Current?.ApplicationLifetime is not IClassicDesktopStyleApplicationLifetime desktop)
            {
                return;
            }
            if (_settingsWindow is null)
            {
                _settingsWindow = new SettingsWindow
                {
                    DataContext = _vm,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner,
                };
                _settingsWindow.Show(desktop.MainWindow);
                _settingsWindow.Closing += (s, e) => _settingsWindow = null;
            }
            else
            {
                _settingsWindow.Activate();
            }
            _= FunctionsHelper.CloseMenus();
            
        }
    }
    
    public void ShowEffectsWindow()
    {
        // TODO: Implement ShowEffectsWindow
    }

    public void ShowResizeWindow()
    {
        // TODO: Implement ShowResizeWindow
    }

    public void Print(string path)
    {
        ProcessHelper.Print(path);
    }

    public void SetAsWallpaper(string path, int wallpaperStyle)
    {
        var style = (WallpaperHelper.WallpaperStyle)wallpaperStyle;
        WallpaperHelper.SetDesktopWallpaper(path, style);
    }
    
    public void SetAsLockScreen(string path)
    {
        // TODO: Run a new instance with admin rights and execute SetLockScreenImage
        LockscreenHelper.SetLockScreenImage(path);
    }

    public void CopyFile(string path)
    {
        ClipboardHelper.CopyFileToClipboard(path);
    }
    
    public async Task<bool> ExtractWithLocalSoftwareAsync(string path, string tempDirectory)
    {
        return await ArchiveExtractionHelper.ExtractWithLocalSoftwareAsync(path, tempDirectory);
    }
    
    #endregion
}