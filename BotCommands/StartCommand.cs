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
        // Тут создаем нашу клавиатуру

        var commands = new List<BotCommand>() {
            new BotCommand() {Command = "/directions", Description = "Список всех доступных направлений"},
            new BotCommand() {Command = "/lessons", Description = "Расписание занятий"}
        };
        // var replyKeyboard = new ReplyKeyboardMarkup(
        //     new List<KeyboardButton[]>()
        //     {
        //         new KeyboardButton[]
        //         {
        //             new KeyboardButton("/start"),
        //             new KeyboardButton("/directions"),
        //             new KeyboardButton("/lessons"),
        //         }
        //     })
        // {
        //     ResizeKeyboard = true
        // };

        await botClient.SetMyCommandsAsync(commands);
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, 
            text: "Выбери команду", replyMarkup: null); 
        
    }
}