using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;

namespace Telegram.Bot.BorisExtensions;

public class CustomClient
{
    public ITelegramBotClient Client;
    private BotController? _controller;
    public CancellationToken Ct;
    public CustomClient(string botToken, CancellationToken cst)
    {
        Client = new TelegramBotClient(botToken);
        Ct = cst;
    }

    public void UseController<T>() where T : BotController, new()
    {
        _controller = new T();
    }

    public async void Start()
    {
        _controller ??= new BotController();
        Client.StartReceiving(
            _controller.HandleAsync,
            _controller.HandleErrorAsync,
            new ReceiverOptions
            {
                AllowedUpdates = _controller.AllowedUpdates
            },
            Ct);
        var me = await Client.GetMeAsync(Ct);
        Console.WriteLine($"Start listening for @{me.Username}");
    }

}