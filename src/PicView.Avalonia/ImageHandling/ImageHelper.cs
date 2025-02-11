﻿using System.Diagnostics;
using Avalonia.Media.Imaging;
using ImageMagick;
using PicView.Avalonia.Navigation;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Core.FileHandling;
using PicView.Core.ImageDecoding;

namespace PicView.Avalonia.ImageHandling;

public static class ImageHelper
{
    public static bool IsAnimated(FileInfo fileInfo)
    {
        var frames = ImageFunctionHelper.GetImageFrames(fileInfo.FullName);
        return frames > 1;
    }
    
    public static async Task<string> ConvertToCommonSupportedFormatAsync(string path, MainViewModel vm)
    {
        Bitmap? source = null;
        
        if (vm.EffectConfig is not null)
        {
            if (vm.ImageSource is Bitmap bmp)
            {
                source = bmp;
            }
        }
        else if (NavigationHelper.CanNavigate(vm) && !string.IsNullOrEmpty(path))
        {
            if (vm.EffectConfig is not null && vm.FileInfo.FullName == path)
            {
                if (vm.ImageSource is Bitmap bmp)
                {
                    source = bmp;
                }
            }
            else if (path == vm.FileInfo.FullName)
            {
                if (path.IsCommon())
                {
                    return path;
                }
                if (vm.ImageSource is Bitmap bmp && vm.FileInfo.FullName.IsSupported())
                {
                    source = bmp;
                }
            }
            else
            {
                var preloadValue = await vm.ImageIterator.GetPreLoadValueAsync(vm.ImageIterator.ImagePaths.IndexOf(path))
                    .ConfigureAwait(false);
                if (preloadValue?.ImageModel.Image is Bitmap bitmap)
                {
                    source = bitmap;
                }
            }
        }
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
        
        if (source is not null)
        {
            await Task.Run(() => source.Save(tempPath));
            return tempPath;
        }
        
        var url = path.GetURL();
        if (!string.IsNullOrWhiteSpace(url))
        {
            // Download the image from the URL
            path = await DownloadImageFromUrlAsync(url);
            if (string.IsNullOrEmpty(path))
            {
                return string.Empty; // If download fails, return empty
            }
        }
        
        if (path.StartsWith("file:///"))
        {
            path = path.Replace("file:///", "");
            path = path.Replace("%20", " ");
        }
        if (!File.Exists(path))
            return string.Empty;
        
        // Convert the image to a common supported format
        // Save image to temp path
        var success = await SaveImageFileHelper.SaveImageAsync(null, path, tempPath, null, null, null, ".png");
        return !success ? string.Empty : tempPath;
    }
    
    
    private static async Task<string> DownloadImageFromUrlAsync(string url)
    {
        // TODO: Refactoring needed: Need to combine with the one in LoadPicFromUrlAsync and add unit tests
        try
        {
            var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + Path.GetExtension(url));

            using var client = new HttpClient();
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                await using var fs = new FileStream(tempPath, FileMode.Create);
                await response.Content.CopyToAsync(fs);
                return tempPath;
            }
        }
        catch (Exception ex)
        {
#if DEBUG
            Trace.WriteLine($"Error downloading image: {ex.Message}");
#endif
            return string.Empty;
        }

        return string.Empty;
    }
    
    public static async Task OptimizeImage(MainViewModel vm)
    {
        if (vm is null)
        {
            return;
        }
        if (!NavigationHelper.CanNavigate(vm))
        {
            return;
        }
        if (vm.FileInfo is null)
        {
            return;
        }
        await Task.Run(() =>
        {
            try
            {
                ImageOptimizer imageOptimizer = new()
                {
                    OptimalCompression = true
                };
                imageOptimizer.LosslessCompress(vm.FileInfo.FullName);
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
            }
        });
        SetTitleHelper.RefreshTitle(vm);
    }
}
