namespace PicView.Core.ImageDecoding;

public static class Base64Helper
{
    /// <summary>
    /// Determines whether a string is a valid Base64 string.
    /// </summary>
    /// <param name="base64">The string to check.</param>
    /// <returns>String as a valid Base64 string; otherwise, "".</returns>
    public static string IsBase64String(string base64)
    {
        if (string.IsNullOrEmpty(base64))
        {
            return "";
        }

        if (base64.StartsWith("data:image/webp;base64,"))
        {
            base64 = base64["data:image/webp;base64,".Length..];
        }

        if (base64.StartsWith("data:image/jpeg;base64,"))
        {
            base64 = base64["data:image/jpeg;base64,".Length..];
        }
        
        if (base64.StartsWith("data:image/png;base64,"))
        {
            base64 = base64["data:image/png;base64,".Length..];
        }
        
        if (base64.StartsWith("data:image/gif;base64,"))
        {
            base64 = base64["data:image/gif;base64,".Length..];
        }

        var buffer = new Span<byte>(new byte[base64.Length]);
        return Convert.TryFromBase64String(base64, buffer, out _) ? base64 : "";
    }
}