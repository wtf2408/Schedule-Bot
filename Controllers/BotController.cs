using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace ScheduleBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        private readonly IConfiguration appConfiguration;
        private TelegramBotClient bot;

        public BotController(IConfiguration configuration)
        {
            this.appConfiguration = configuration;
            bot = Bot.GetBot(configuration["Token"]);
        }

        [HttpPost]
        public async void Post(Telegram.Bot.Types.Update update) //Сюда будут приходить апдейты
        {
            long chatId = update.Message.Chat.Id; //получаем айди чата, куда нам сказать привет
            await bot.SendTextMessageAsync(chatId, "Привет!");
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
