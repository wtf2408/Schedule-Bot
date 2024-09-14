using System;
using Telegram.Bot;

namespace ScheduleBot;

public class Bot
{
    private static TelegramBotClient client { get; set; }

    public static TelegramBotClient GetBot(string token)
    {
        if (client != null)
        {
            return client;
        }
        client = new TelegramBotClient(token);
        return client;
    }
}
