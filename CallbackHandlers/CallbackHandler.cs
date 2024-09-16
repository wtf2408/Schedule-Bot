using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScheduleBot.CallbackHandlers;

public class CallbackHandler
{
    private TelegramBotClient botClient;
    // private Message lastMessage;
    public CallbackHandler(TelegramBotClient client)
    {
        botClient = client;
        System.Console.WriteLine("меня создали - CallbackHandler");
    }
    
    public async void Handle(CallbackQuery callback, ScheduleBotContext context)
    {
        long chatId = callback.Message.Chat.Id;
        string response = string.Empty;

        var day = JsonConvert.DeserializeObject<DayOfWeek>(callback.Data);
        var daySchedule = from entry in context.Schedule.Include(e => e.Direction) 
                          where entry.Day == day orderby entry.Time 
                          select entry;

        foreach (var entry in daySchedule)
        {
            response += $"{entry.Time} {entry?.Direction?.Name}\n";
        }
        //await botClient.DeleteMessageAsync(callback.Message.Chat.Id, callback.Message.MessageId);
        await botClient.SendTextMessageAsync(chatId, $"Расписание занятий в этот день:\n{response}");
        await botClient.AnswerCallbackQueryAsync(callback.Id);
    } 
}
