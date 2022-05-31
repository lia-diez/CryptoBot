using System.Net.Http.Json;
using CryptoBot.Models;
using Telegram.Bot;
using Telegram.Bot.BorisExtensions;
using Telegram.Bot.BorisExtensions.Attributes;

namespace CryptoBot;

public class Controller : BotController
{
    private HttpClient _client = new();
    private TelebotContext _db = TelebotContext.Instance;
    private User? _user => _db.Users.FirstOrDefault(a => a.Id == BotUpdate.Message.From.Id);

    public Controller()
    {
        Users = new List<User>();
    }
    

    [DefaultCommand]
    [BotCommand("/help")]
    public async Task Help(params string[] parameters)
    {
        await BotClient.SendTextMessageAsync(
            chatId: ChatId,
            text: "Test help message",
            cancellationToken: CancellationToken);
    }

    [BotCommand("/start")]
    public async Task Start(params string[] parameters)
    {
        await BotClient.SendTextMessageAsync(
            chatId: ChatId,
            text: BotUpdate.Message.From.Username,
            cancellationToken: CancellationToken);
    }
    
    [BotCommand("/getcrypto")]
    public async Task GetCrypto(params string[] parameters)
    {
        string message;
        if (parameters.Length == 0) message = "Долбоеб название введи";
        else
        {
            var currency = await _client.GetFromJsonAsync<Currency>($"http://localhost/Crypto?currency={parameters[0]}");
            message = $"Курс {currency?.Name} на данный момент: \n{currency?.ExchangeRate}USD";
        }

        await BotClient.SendTextMessageAsync(
            chatId: ChatId,
            text: message,
            cancellationToken: CancellationToken);
    }

    [BotCommand("/reg")]
    public async Task Register(params string[] parameters)
    {
        _db.Users.Add(new User
        {
            TelegramId = BotUpdate.Message.From.Id,
            UserName = BotUpdate.Message.From.Username,
            ChatId = BotUpdate.Message.Chat.Id,
            FirstName = BotUpdate.Message.From.FirstName
        });
        await _db.SaveChangesAsync();
    }
}