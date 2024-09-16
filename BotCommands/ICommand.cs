using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace ScheduleBot.BotCommands;
public interface ICommand 
{
    public TelegramBotClient botClient { get; set; }
    public string Name { get; }
    public void Execute(Update update, DbContext dbContext = null);
}
