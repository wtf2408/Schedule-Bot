using System;
using Microsoft.EntityFrameworkCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace ScheduleBot.BotCommands;

public class DirectionsCommand : ICommand
{
    public TelegramBotClient botClient { get; set; }
    public string Name => "/directions";
    public async void Execute(Update update, DbContext dbContext = null)
    {
        string response = string.Empty;
        if (dbContext is ScheduleBotContext scheduleBotContext)
        foreach (var d in scheduleBotContext.Directions) response += $"{d.Name}\n";
            await botClient.SendTextMessageAsync(update.Message.Chat.Id, $"Список направлений:\n{response}");
    }
}
