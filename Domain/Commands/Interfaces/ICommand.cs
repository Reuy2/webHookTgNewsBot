using Telegram.Bot.Types;
using Telegram.Bot;

namespace WorkOutWebHookBot.Domain.Commands.Interfaces
{
    public interface ICommand
    {
        public TelegramBotClient Client { get; }

        public string Name { get; }

        public Task Execute(Update update);
    }
}
