using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.BorisExtensions.Attributes;

[AttributeUsage(AttributeTargets.Method)]
public class BotCommandAttribute : Attribute
{
    public BotCommandData CommandData;
    public BotCommandAttribute(string command = "", string state = "", UpdateType updateType = UpdateType.Message)
    {
        CommandData = new BotCommandData
        {
            Command = command.ToLower(),
            UpdateType = updateType,
            State = state
        };
    }
}