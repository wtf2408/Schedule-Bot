using System;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.CallbackHandlers;

public class CallbackHandler
{
    private TelegramBotClient botClient;
    // private Message lastMessage;
    public CallbackHandler(TelegramBotClient client)
    {
        botClient = client;
    }
    
    public async void Handle(CallbackQuery callback, ScheduleBotContext context)
    {
        try
        {
            var prevMessage = callback.Message;
            string response = "В этот день нет занятий";

            var day = JsonConvert.DeserializeObject<DayOfWeek>(callback.Data);
            var daySchedule = (from entry in context.Schedule.Include(e => e.Subject)
                               where entry.Day == day
                               orderby entry.StartTime
                               select entry).ToList();
                               
            if (daySchedule.Count == 0) response = GetDivider(response.Length) + response;
            else
            {
                int dividerLen = (from entry in daySchedule select entry.Subject.Name.Length).Max();
                string divider = GetDivider(dividerLen);
                response = divider;
                foreach (var entry in daySchedule)
                {
                    response += $"{entry.StartTime} - {entry.StartTime.Add(new TimeSpan(hours: 1, minutes: 20, seconds: 0))}\n {entry?.Subject?.Name}\n {entry.LessonType}\n{divider}";
                }
            }
            await botClient.EditMessageTextAsync(prevMessage.Chat.Id, 
                                                 messageId: prevMessage.MessageId,
                                                 text: $"{day}\n{response}", 
                                                 replyMarkup: prevMessage.ReplyMarkup);
            await botClient.AnswerCallbackQueryAsync(callback.Id);
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e.Message);
        }
    } 

    private string GetDivider(int dashCount)
    {
        string dash = string.Empty;
        for (int i = 0; i < dashCount; i++) dash += "--";
        return dash + "\n";
    }
}
