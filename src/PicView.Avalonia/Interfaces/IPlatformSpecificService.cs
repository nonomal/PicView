﻿namespace PicView.Avalonia.Interfaces;

public interface IPlatformSpecificService
{
    void SetTaskbarProgress(ulong progress, ulong maximum);
    void StopTaskbarProgress();
    void SetCursorPos(int x, int y);

    List<string> GetFiles(FileInfo fileInfo);

    int CompareStrings(string str1, string str2);

    void OpenWith(string path);

    void LocateOnDisk(string path);
    
    void ShowFileProperties(string path);

    void ShowAboutWindow();

    void ShowExifWindow();

    void ShowKeybindingsWindow();

    void ShowSettingsWindow();
    
    void ShowEffectsWindow();
    
    void ShowResizeWindow();
    
    void Print(string path);
    
    void SetAsWallpaper(string path, int wallpaperStyle);
    
    void SetAsLockScreen(string path);
    
    void CopyFile(string path);
    
    Task<bool> ExtractWithLocalSoftwareAsync(string path, string tempDirectory);
}