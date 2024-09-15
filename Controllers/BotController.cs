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
            string response = string.Empty;

            switch (update.Type)
            {
                case UpdateType.CallbackQuery:
                    await bot.AnswerCallbackQueryAsync(update.CallbackQuery.Id, "Ответ на колбек");
                    long chat_id = update.CallbackQuery.Message.Chat.Id;
                    var message_id = update.CallbackQuery.Message.MessageId;
                    await bot.DeleteMessageAsync(chat_id, message_id);
                    await bot.SendTextMessageAsync(chat_id, "ответ на колбек заебал уже давай работай");
                    break;
                case UpdateType.Message:
                    long chatId = update.Message.Chat.Id; //получаем айди чата, куда нам сказать привет
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

                            await bot.SendTextMessageAsync(chatId, text: "Выбери команду",
                                replyMarkup: replyKeyboard); // передаем клавиатуру в параметр replyMarkup
                            return;
                            
                        case "/directions":
                            foreach (var d in scheduleContext.Directions) response += $"{d.Name}\n";
                            await bot.SendTextMessageAsync(chatId, $"Список направлений:\n{response}");
                            break;

                        case "/lessons":
                            var inlineKeyboard = new InlineKeyboardMarkup(
                                new List<InlineKeyboardButton>
                                {
                                    new InlineKeyboardButton("Пн") {CallbackData = "/mn"},
                                    new InlineKeyboardButton("Вт") {CallbackData = "/tu"},
                                }
                            );
                            await bot.SendTextMessageAsync(chatId, text: "Выбирете день:",
                                replyMarkup: inlineKeyboard);
                            break;
                    }
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
