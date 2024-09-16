using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using Newtonsoft.Json;

namespace ScheduleBot.BotCommands;

public class LessonsCommand : ICommand
{
    public TelegramBotClient botClient { get; set; }
    public string Name => "/lessons";
    public async void Execute(Update update, DbContext dbContext = null)
    {
        
         var inlineKeyboard = new InlineKeyboardMarkup(
            new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Пн") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Monday)},
                new InlineKeyboardButton("Вт") {CallbackData = "2"},
                new InlineKeyboardButton("Ср") {CallbackData = "3"},
                new InlineKeyboardButton("Чт") {CallbackData = "4"},
            }
        );
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text: "Выбирете день:",
            replyMarkup: inlineKeyboard);
    }
}
