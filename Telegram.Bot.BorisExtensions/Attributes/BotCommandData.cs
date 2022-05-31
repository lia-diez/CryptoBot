using Telegram.Bot.Types.Enums;

namespace Telegram.Bot.BorisExtensions.Attributes;

public struct BotCommandData
{
    public string Command;
    public UpdateType UpdateType;
    public string State;
}