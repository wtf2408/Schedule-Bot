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
        private TelegramBotClient bot;
        private ScheduleBotContext scheduleContext;

        public BotController(IConfiguration configuration, ScheduleBotContext context)
        {
            this.appConfiguration = configuration;
            bot = Bot.GetBot(configuration["Token"]);
            scheduleContext = context;
        }

        [HttpPost]
        public async void Post(Telegram.Bot.Types.Update update) //Сюда будут приходить апдейты
        {
            long chatId = update.Message.Chat.Id; //получаем айди чата, куда нам сказать привет
            string response = string.Empty;


            switch (update.Message.Text)
            {
                case "/start":
                    // Тут создаем нашу клавиатуру
                    var replyKeyboard = new ReplyKeyboardMarkup(
                        new List<KeyboardButton[]>()
                        {
                            new KeyboardButton[]
                            {
                                new KeyboardButton("/start"),
                                new KeyboardButton("/directions"),
                                new KeyboardButton("/lessons"),
                            }
                        })
                    {
                        ResizeKeyboard = true
                    };

                    await bot.SendTextMessageAsync(chatId,"Выбери команду",
                        replyMarkup: replyKeyboard); // передаем клавиатуру в параметр replyMarkup
                    return;
                    
                case "/directions":
                    foreach (var d in scheduleContext.Directions) response += $"{d.Name}\n";
                    await bot.SendTextMessageAsync(chatId, $"Список направлений:\n{response}");
                    break;
            }


        }
        [HttpGet]
        public string Get() 
        {
            //Здесь мы пишем, что будет видно если зайти на адрес,
            //указаную в ngrok и launchSettings
            return "Telegram bot was started";
        }

    }
}
