using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace ScheduleBot.Controllers
{
    [Route("/")]
    [ApiController]
    public class BotController : ControllerBase
    {
        [HttpPost]
        public void Post(Telegram.Bot.Types.Update update) //Сюда будут приходить апдейты
        {
            Console.WriteLine(update?.Message?.Text);
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
