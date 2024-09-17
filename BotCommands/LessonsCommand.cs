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
            [ 
                [
                    new InlineKeyboardButton("Пн") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Monday)},
                    new InlineKeyboardButton("Вт") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Tuesday)},
                    new InlineKeyboardButton("Ср") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Wednesday)}
                ],
                [
                    new InlineKeyboardButton("Чт") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Thursday)},
                    new InlineKeyboardButton("Пт") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Friday)},
                    new InlineKeyboardButton("Сб") {CallbackData = JsonConvert.SerializeObject(DayOfWeek.Saturday)}
                ]
            ]
        );
        await botClient.SendTextMessageAsync(update.Message.Chat.Id, text: "Выбирете день:",
            replyMarkup: inlineKeyboard);
    }
}
