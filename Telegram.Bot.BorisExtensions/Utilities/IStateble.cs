namespace Telegram.Bot.BorisExtensions.Utilities;

public interface IStateUser
{
    public long TelegramId { get; set; }
    public string? State { get; set; }
}