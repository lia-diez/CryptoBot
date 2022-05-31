using System.Text.RegularExpressions;

namespace Telegram.Bot.BorisExtensions.Utilities;

public static class Utilities
{
    public static Regex ComandRegex = new ("^/\\w+", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static string GetCommand(this string? str)
    {
        return ComandRegex.Match(str).Value;
    }
}