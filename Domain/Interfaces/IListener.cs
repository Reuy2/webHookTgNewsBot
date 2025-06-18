using Telegram.Bot.Types;
using WorkOutWebHookBot.Domain.Executors;

namespace WorkOutWebHookBot.Domain.Interfaces
{
    public interface IListener
    {
        public Task GetUpdate(Update update);

        public CommandExecutor Executor { get; }
    }
}
