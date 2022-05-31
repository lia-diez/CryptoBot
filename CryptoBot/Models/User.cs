using Telegram.Bot.BorisExtensions;
using Telegram.Bot.BorisExtensions.Utilities;

namespace CryptoBot.Models;

public class User : IStateUser
{
    public int Id { get; set; }
    public long TelegramId { get; set; }
    public string? State { get; set; }
    public long ChatId { get; set; }
    public string? UserName { get; set; }
    public string? FirstName { get; set; }
}