using System;
using Telegram.Bot;
using ScheduleBot.BotCommands;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using ScheduleBot.CallbackHandlers;

namespace ScheduleBot;

public class Bot
{
    private static Bot bot { get; set; }
    public List<ICommand> Commands;
    public CallbackHandler CallbackHandler { get; }
    public TelegramBotClient client;

    private Bot(string token)
    {
        client = new TelegramBotClient(token);

        Commands = new List<ICommand>
        {
            new StartCommand(),
            new LessonsCommand()
        };
        this.CallbackHandler = new CallbackHandler(client);
        foreach (var command in Commands) command.botClient = client;
    }

    public static Bot GetBot(string token)
    {
        if (bot != null)
        {
            return bot;
        }
        bot = new Bot(token);
        return bot;
    }
}
