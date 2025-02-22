using System.Reflection;

namespace PicView.Core.Config;

public static class VersionHelper
{
    public static string? GetCurrentVersion()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            var informationVersion = assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>().InformationalVersion;
            return informationVersion[..informationVersion.IndexOf('+')];
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            var assembly = Assembly.GetExecutingAssembly();
            var assemblyVersion = assembly.GetName().Version;
            return $"{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}.{assemblyVersion.Revision}";
        }
    }

    public static Version? GetAssemblyVersion()
    {
        try
        {
            var assembly = Assembly.GetExecutingAssembly();
            return assembly.GetName().Version;
        }
        catch (Exception e)
        {
#if DEBUG
            Console.WriteLine(e);
#endif
            return null;
        }
    }
}