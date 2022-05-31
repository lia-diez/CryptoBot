namespace Telegram.Bot.BorisExtensions.Attributes;

public class BotMailingAttribute : Attribute
{
    public BotMailingData BotMailingData;

    public BotMailingAttribute(TimeOnly time, string category)
    {
        BotMailingData = new BotMailingData
        {
            Time = time,
            Category = category
        };
    }
}