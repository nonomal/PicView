using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Win32;
using PicView.Avalonia.UI;
using PicView.Avalonia.ViewModels;
using PicView.Avalonia.WindowBehavior;
using PicView.Core.Config;
using PicView.Core.FileHandling;

namespace PicView.Avalonia.Update;

[JsonSourceGenerationOptions(AllowTrailingCommas = true)]
[JsonSerializable(typeof(UpdateInfo))]
public partial class UpdateSourceGenerationContext : JsonSerializerContext;

public static class UpdateManager
{
    public static async Task UpdateCurrentVersion(MainViewModel vm)
    {
        // TODO Add support for other OS
        // TODO add UI
        var currentDirectory = new DirectoryInfo(Environment.ProcessPath);

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            bool isAdmin;
            try
            {
                isAdmin = currentDirectory.GetAccessControl().AreAccessRulesProtected;
            }
            catch (Exception)
            {
                isAdmin = false;
            }

            if (isAdmin)
            {
                // Restart the application as admin
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        Verb = "runas",
                        UseShellExecute = true,
                        FileName = "PicView.exe",
                        Arguments = "update",
                        WorkingDirectory = AppDomain.CurrentDomain.BaseDirectory
                    }
                };
                process.Start();
                Environment.Exit(0);
            }
        }

        // ReSharper disable once RedundantAssignment
        var currentVersion = VersionHelper.GetAssemblyVersion();
        const string url = "https://picview.org/update.json";
        const string backUpUrl  = "https://picview.netlify.app/update.json";
        var tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
        Directory.CreateDirectory(tempPath);
        var tempJsonFileDestination = Path.Combine(tempPath, "update.json");

#if DEBUG
        // Change it to lower to test it
        currentVersion = new Version("3.0.0.3");
#endif

        // Download the JSON file
        using var jsonFileDownloader = new HttpHelper.HttpClientDownloadWithProgress(url, tempJsonFileDestination);
        try
        {
            await jsonFileDownloader.StartDownloadAsync();
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            try
            {
                using var retryJsonFileDownloader = new HttpHelper.HttpClientDownloadWithProgress(backUpUrl, tempJsonFileDestination);
                await jsonFileDownloader.StartDownloadAsync();
            }
            catch (Exception exception)
            {
#if DEBUG
                Console.WriteLine(exception);
#endif
                await TooltipHelper.ShowTooltipMessageAsync(exception.Message);
                return;
            }
        }

        // Read and deserialize the JSON
        UpdateInfo? updateInfo;
        try
        {
            var jsonString = await File.ReadAllTextAsync(tempJsonFileDestination);
            if (JsonSerializer.Deserialize(
                    jsonString, typeof(UpdateInfo),
                    UpdateSourceGenerationContext.Default) is not UpdateInfo serializedUpdateInfo)
            {
#if DEBUG
                Console.WriteLine("Update information is missing or corrupted.");
#endif
                await TooltipHelper.ShowTooltipMessageAsync("Update information is missing or corrupted.");
                return;
            }

            updateInfo = serializedUpdateInfo;
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            await TooltipHelper.ShowTooltipMessageAsync("Failed to parse update information: \n" + e.Message);
            return;
        }

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            var isInstalled = CheckIfIsInstalled();

            var architecture = RuntimeInformation.ProcessArchitecture switch
            {
                Architecture.X64 => isInstalled ? InstalledArchitecture.X64Install : InstalledArchitecture.X64Portable,
                Architecture.Arm64 => isInstalled
                    ? InstalledArchitecture.Arm64Install
                    : InstalledArchitecture.Arm64Portable,
                _ => InstalledArchitecture.X64Install
            };

            var remoteVersion = new Version(updateInfo.Version);
            if (remoteVersion <= currentVersion)
            {
                return;
            }

            switch (architecture)
            {
                case InstalledArchitecture.Arm64Install:
                    // Launch the installer and close the window
                    var fileName = Path.GetFileName(updateInfo.X64Install);
                    var tempFileDownloadPath = Path.Combine(tempPath, fileName);
                    await StartFileDownloader(vm, updateInfo.Arm64Install, tempFileDownloadPath);
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            UseShellExecute = true,
                            FileName = tempFileDownloadPath
                        }
                    };
                    process.Start();
                    await WindowFunctions.WindowClosingBehavior();
                    return;
                case InstalledArchitecture.Arm64Portable:
                    // Download the zip package in browser
                    process = new Process
                    {
                        StartInfo = new ProcessStartInfo(updateInfo.Arm64Portable)
                        {
                            UseShellExecute = true,
                            Verb = "open"
                        }
                    };
                    process.Start();
                    await process.WaitForExitAsync();
                    return;
                case InstalledArchitecture.X64Install:
                    // Launch the installer and close the window
                    fileName = Path.GetFileName(updateInfo.X64Install);
                    tempFileDownloadPath = Path.Combine(tempPath, fileName);
                    await StartFileDownloader(vm, updateInfo.X64Install, tempFileDownloadPath);
                    process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            UseShellExecute = true,
                            FileName = tempFileDownloadPath
                        }
                    };
                    process.Start();
                    await WindowFunctions.WindowClosingBehavior();
                    return;
                case InstalledArchitecture.X64Portable:
                    
                    // Download the zip package in browser
                    process = new Process
                    {
                        StartInfo = new ProcessStartInfo(updateInfo.X64Portable)
                        {
                            UseShellExecute = true,
                            Verb = "open"
                        }
                    };
                    process.Start();
                    await process.WaitForExitAsync();
                    return;
            }
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            // TODO    
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            // TODO
        }
    }

    private static bool CheckIfIsInstalled()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return false;
        }

        var x64Path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "PicView.exe";
        if (File.Exists(x64Path))
        {
            return true;
        }

        const string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
        try
        {
            using var key = Registry.LocalMachine.OpenSubKey(registryKey);
            if (key == null)
            {
                return false;
            }

            foreach (var subKeyName in key.GetSubKeyNames())
            {
                using var subKey = key.OpenSubKey(subKeyName);

                var installDir = subKey?.GetValue("InstallLocation")?.ToString();
                if (installDir == null)
                {
                    continue;
                }

                return Path.Exists(Path.Combine(installDir, "PicView.exe"));
            }
        }
        catch (Exception e)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(CheckIfIsInstalled)} exception, \n {e.Message}");
#endif
            return false;
        }

        return false;
    }

    private static async Task StartFileDownloader(MainViewModel vm, string downloadUrl, string tempPath)
    {
        vm.PlatformService.StopTaskbarProgress();
        using var jsonFileDownloader = new HttpHelper.HttpClientDownloadWithProgress(downloadUrl, tempPath);
        try
        {
            jsonFileDownloader.ProgressChanged += (size, downloaded, percentage) =>
                ProgressChanged(vm, size, downloaded, percentage);
            await jsonFileDownloader.StartDownloadAsync();
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            await TooltipHelper.ShowTooltipMessageAsync(e.Message);
        }
        finally
        {
            vm.PlatformService.StopTaskbarProgress();
        }
    }

    private static void ProgressChanged(MainViewModel vm, long? totalfilesize, long? totalbytesdownloaded,
        double? progresspercentage)
    {
        if (totalfilesize is null || totalbytesdownloaded is null || progresspercentage is null)
        {
            return;
        }

        vm.PlatformService.SetTaskbarProgress((ulong)totalbytesdownloaded, (ulong)totalfilesize);
    }

    private enum InstalledArchitecture
    {
        X64Portable,
        X64Install,
        Arm64Portable,
        Arm64Install
    }
}