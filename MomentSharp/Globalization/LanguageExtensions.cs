using MomentSharp.Globalization.Languages;

namespace MomentSharp.Globalization;

public static class LanguageExtensions
{
    private static ILocalize? _language;
    public static ILocalize Language => _language ??= SetLanguageByCulture();

    public static ILocalize GetLanguage(this DateTime dateTime)
    {
        return Language;
    }
    
    /// <summary>
    /// Attempts to find the correct <see cref="ILocalize"/> based on the <see cref="Thread.CurrentThread"/> CurrentCulture
    /// </summary>
    /// <returns>ILocalize</returns>
    private static ILocalize SetLanguageByCulture()
    {
        var culture = Thread.CurrentThread.CurrentCulture.ToString().Replace("-", "");
        return culture switch
        {
            "enUS" => new EnUs(),
            "de" => new De(),
            "zhCN" => new ZhCn(),
            "zhTW" => new ZhTw(),
            _ => new EnUs()
        };
    }
}