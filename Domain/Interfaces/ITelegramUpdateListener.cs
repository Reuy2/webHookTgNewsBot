using Telegram.Bot.Types;

namespace WorkOutWebHookBot.Domain.Interfaces
{
    public interface ITelegramUpdateListener
    {
        public Task GetUpdate(Update update);
    }
}
