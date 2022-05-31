namespace Telegram.Bot.BorisExtensions.Attributes;

public class MailCategoryAttribute
{
    public string Category;
    public MailCategoryAttribute(string category)
    {
        Category = category;
    }
}