namespace CryptoBot;

public static class Utilities
{
    public static string GetToken()
    {
        var token = Environment.GetEnvironmentVariable("BOT_TOKEN");
        if (token == null) throw new ArgumentException("Bot token not found");
        return token;
    }
}