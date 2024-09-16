using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using System.Linq;
using Telegram.Bot.Types.ReplyMarkups;

namespace ScheduleBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IConfiguration appConfiguration;
        private ScheduleBotContext scheduleContext;
        private Bot bot;

        public BotController(IConfiguration configuration, ScheduleBotContext context)
        {
            appConfiguration = configuration;
            scheduleContext = context;

            bot = Bot.GetBot(configuration["Token"]);
        }

        [HttpPost]
        public async void Post(Update update) //Сюда будут приходить апдейты
        {
            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    bot.CallbackHandler.Handle(update.CallbackQuery, scheduleContext);
                    break;

                
                case UpdateType.Message:
                    foreach (var command in bot.Commands)
                    {
                        if (command.Name == update.Message.Text)
                            command.Execute(update, scheduleContext);
                    }
                    break;
            }


        }
        [HttpGet]
        public string Get() 
        {
            return "Telegram bot was started";
        }

    }
}
