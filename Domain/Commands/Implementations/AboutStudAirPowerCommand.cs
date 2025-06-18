using WorkOutWebHookBot.BotStrings.Answers;
using WorkOutWebHookBot.BotStrings.Commands;
using WorkOutWebHookBot.BotStrings.Keyboards;
using WorkOutWebHookBot.Domain.Commands.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace WorkOutWebHookBot.Domain.Commands.Implementations
{
    public class AboutStudAirPowerCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name => BaseCommands.StudAirPowerAbout;

        public async Task Execute(Update update)
        {
            try
            {
                await Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    BaseAnswers.AboutStudAirPower
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
