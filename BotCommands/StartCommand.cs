using Microsoft.EntityFrameworkCore;
using ScheduleBot;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.BotCommands;
public class StartCommand : ICommand
{
    public string Name => "/start";
    public TelegramBotClient botClient { get; set; }
    public async void Execute(Update update, DbContext dbContext = null) {

        var commands = new List<BotCommand>() {
            new BotCommand() {Command = "/start", Description = "Старт"},
            new BotCommand() {Command = "/lessons", Description = "Расписание занятий"}
        };

        await botClient.SetMyCommandsAsync(commands);
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
            text: "Выбери команду из меню,\nкоторое находится слева от поля ввода", replyMarkup: null); 
        
    }
}