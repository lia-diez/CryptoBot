using System.Reflection;
using Telegram.Bot.BorisExtensions.Attributes;
using Telegram.Bot.BorisExtensions.Utilities;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.BorisExtensions;

public class BotController
{
    public long ChatId => BotUpdate.Message!.Chat.Id;

    protected ITelegramBotClient BotClient;
    protected Update BotUpdate;
    protected CancellationToken CancellationToken;
    public UpdateType[] AllowedUpdates;
    private Dictionary<BotCommandData, Delegates.CommandDelegate> Methods;
    private Dictionary<BotMailingData, Delegates.MailingDelegate> Mailing;
    private Delegates.CommandDelegate _default;
    protected IEnumerable<IStateUser> Users;
    
    public BotController()
    {
        Methods = new Dictionary<BotCommandData, Delegates.CommandDelegate>();
        var allowed = new List<UpdateType>();
        foreach (var method in GetType().GetMethods())
        {
            var command = method.GetCustomAttribute<BotCommandAttribute>();
            if (command != null)
            {
                Delegates.CommandDelegate del;
                try
                {
                    del = method.CreateDelegate<Delegates.CommandDelegate>(this);
                }
                catch (ArgumentException)
                {
                    continue;
                }
                Methods.Add(command.CommandData, del);
                allowed.Add(command.CommandData.UpdateType);
            }
            var defaultCommand = method.GetCustomAttribute<DefaultCommandAttribute>();
            if (defaultCommand != null)
                _default = method.CreateDelegate<Delegates.CommandDelegate>(this);
        }

        if (_default == null) throw new Exception("No default method found, use [DefaultCommand] attribute");
        AllowedUpdates = allowed.ToArray();

    }


    public async Task HandleAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        BotClient = botClient;
        BotUpdate = update;
        CancellationToken = cancellationToken;
        await Route(botClient, update, cancellationToken);
        await CustomHandle(botClient, update, cancellationToken);
    }

    public async Task CustomHandle(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
    }

    public async Task Route(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var message = update.Message?.Text ?? update.EditedMessage?.Text;
        var command = message.GetCommand();
        var state = Users.FirstOrDefault(a => a.TelegramId == update.Message?.From?.Id)?.State ?? "";
        var method = Methods.GetValueOrDefault(new BotCommandData
        {
            Command = command,
            UpdateType = update.Type,
            State = state
        }) ?? _default;
        await method.Invoke(message?.Split(' ', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>());
    }

    public Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        var errorMessage = exception switch
        {
            ApiRequestException apiRequestException
                => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };
        Console.WriteLine(errorMessage);
        return Task.CompletedTask;
    }
}