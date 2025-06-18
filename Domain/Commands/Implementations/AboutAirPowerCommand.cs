using Telegram.Bot;
using Telegram.Bot.Types;
using WorkOutWebHookBot.BotStrings.Answers;
using WorkOutWebHookBot.BotStrings.Commands;
using WorkOutWebHookBot.BotStrings.Keyboards;
using WorkOutWebHookBot.Domain.Commands.Interfaces;

namespace WorkOutWebHookBot.Domain.Commands.Implementations
{
    public class AboutAirPowerCommand : ICommand
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name => BaseCommands.AirPowerAbout;

        public async Task Execute(Update update)
        {
            try
            {
                await Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    BaseAnswers.AboutAirPower);
            }
            catch (Exception ex)
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
