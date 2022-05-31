using Telegram.Bot.Extensions.Polling;

namespace Telegram.Bot.BorisExtensions.Utilities;

public static class Delegates
{
    public delegate Task CommandDelegate(params string[] parameters);

    public delegate Task MailingDelegate();
}