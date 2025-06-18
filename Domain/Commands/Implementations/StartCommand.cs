using Telegram.Bot.Types;
using Telegram.Bot;
using WorkOutWebHookBot.Domain.Commands.Interfaces;
using WorkOutWebHookBot.BotStrings.Answers;
using WorkOutWebHookBot.BotStrings.Keyboards;
using WorkOutWebHookBot.BotStrings.Commands;

namespace WorkOutWebHookBot.Domain.Commands.Implementations
{
    public class StartCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name => BaseCommands.Start;

        public async Task Execute(Update update)
        {
            try
            {
                long chatId = update.Message.Chat.Id;
                await Client.SendTextMessageAsync(
                    chatId,
                    BaseAnswers.StartString
                    );
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                await Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    BaseAnswers.Error
                    );
            }
        }
    }
}
