using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;
using Telegram.Bot;
using WorkOutWebHookBot.Domain.Distributors;
using WorkOutWebHookBot.Domain.Executors;
using WorkOutWebHookBot.Domain;

namespace WorkOutWebHookBot.Controllers
{
    [ApiController]
    [Route("/")]
    public class BotController : ControllerBase
    {
        private static UpdateDistributor<CommandExecutor> updateDistributor = new UpdateDistributor<CommandExecutor>();

        [HttpPost]
        public async void Post(Update update)
        {
            Console.WriteLine(update.Message.Text);
            if (update.Message == null) //и такое тоже бывает, делаем проверку
                return;

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                await updateDistributor.GetUpdate(update);
            }

            GC.Collect();
        }

        [HttpGet]
        public void Get()
        {
            //return "Telegram bot was started";
        }
    }
}
