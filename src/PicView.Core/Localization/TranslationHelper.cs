using System.Diagnostics;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;
using PicView.Core.Config;

namespace PicView.Core.Localization;

[JsonSourceGenerationOptions(AllowTrailingCommas = true)]
[JsonSerializable(typeof(LanguageModel))]
internal partial class LanguageSourceGenerationContext : JsonSerializerContext;

/// <summary>
/// Helper class for managing language-related tasks.
/// </summary>
public static class TranslationHelper
{
    public static LanguageModel? Translation
    {
        get;
        private set;
    }

    public static void Init()
    {
        Translation = new LanguageModel();
    }

    public static string GetTranslation(string key)
    {
        if (Translation == null)
        {
            return key;
        }

        var propertyInfo = typeof(LanguageModel).GetProperty(key);
        return propertyInfo?.GetValue(Translation) as string ?? key;
    }

    /// <summary>
    /// Determines the language based on the specified culture and loads the corresponding language file.
    /// </summary>
    /// <param name="isoLanguageCode">The culture code representing the desired language.</param>
    public static async Task<bool> LoadLanguage(string isoLanguageCode)
    {
        var jsonLanguageFile = DetermineLanguageFilePath(isoLanguageCode);

        try
        {
            await LoadLanguageFromFileAsync(jsonLanguageFile).ConfigureAwait(false);
            return true;
        }
        catch (FileNotFoundException fnfEx)
        {
#if DEBUG
            Trace.WriteLine($"Language file not found: {fnfEx.Message}");
#endif
            return false;
        }
        catch (Exception ex)
        {
#if DEBUG
            Trace.WriteLine($"{nameof(LoadLanguage)} exception:\n{ex.Message}");
#endif
            return false;
        }
    }

    public static async Task DetermineAndLoadLanguage()
    {
        var isoLanguageCode = DetermineCorrectLanguage();
        SettingsHelper.Settings.UIProperties.UserLanguage = isoLanguageCode;
        await LoadLanguage(isoLanguageCode).ConfigureAwait(false);
    }

    public static IEnumerable<string> GetLanguages()
    {
        var languagesDirectory = GetLanguagesDirectory();
        return Directory.EnumerateFiles(languagesDirectory, "*.json", SearchOption.TopDirectoryOnly);
    }

    public static async Task ChangeLanguage(int language)
    {
        var choice = (Languages)language;
        var languageCode = choice.ToString().Replace('_', '-');
        SettingsHelper.Settings.UIProperties.UserLanguage = languageCode;
        await LoadLanguage(languageCode).ConfigureAwait(false);
        await SettingsHelper.SaveSettingsAsync().ConfigureAwait(false);
    }

    private static string DetermineLanguageFilePath(string isoLanguageCode)
    {
        var languagesDirectory = GetLanguagesDirectory();
        var matchingFiles = Directory.GetFiles(languagesDirectory, "*.json")
            .Where(file => Path.GetFileNameWithoutExtension(file)?.Equals(isoLanguageCode, StringComparison.OrdinalIgnoreCase) == true)
            .ToList();

        return matchingFiles.FirstOrDefault() ?? Path.Combine(languagesDirectory, "en.json");
    }

    private static async Task LoadLanguageFromFileAsync(string filePath)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"Language file not found: {filePath}");
        }

        var jsonString = await File.ReadAllTextAsync(filePath).ConfigureAwait(false);
        var language = JsonSerializer.Deserialize(jsonString, typeof(LanguageModel), LanguageSourceGenerationContext.Default) as LanguageModel;
        Translation = language;
    }

    private static string GetLanguagesDirectory()
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Config/Languages/");
    }
    
    public static string DetermineCorrectLanguage()
    {
        var userCulture = CultureInfo.CurrentUICulture;
        var baseLanguageCode = userCulture.TwoLetterISOLanguageName; // Gets 'da' from 'da-DK'

        // Handle special cases, e.g., Chinese or different regions.
        switch (baseLanguageCode)
        {
            case "zh":
                // Simplified Chinese vs Traditional Chinese
                return userCulture.Name switch
                {
                    "zh-TW" => "zh-TW",  // Traditional Chinese
                    "zh-HK" => "zh-TW",  // Treat Hong Kong as Traditional Chinese
                    _ => "zh-CN"         // Default to Simplified Chinese
                };
            case "de":
                // Handle German-speaking regions (Austria, Germany, Switzerland)
                return "de";  // Map all 'de-*' to 'de'
            default:
                // Fall back to the base language if it's available in the translation files
                return GetLanguages()
                    .Any(lang => Path.GetFileNameWithoutExtension(lang) == baseLanguageCode)
                    ? baseLanguageCode
                    : "en"; // Default to English if not found
        }
    }
}
