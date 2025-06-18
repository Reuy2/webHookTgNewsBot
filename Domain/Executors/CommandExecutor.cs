using Telegram.Bot.Types;
using WorkOutWebHookBot.Domain.Commands.Implementations;
using WorkOutWebHookBot.Domain.Commands.Interfaces;
using WorkOutWebHookBot.Domain.Interfaces;

namespace WorkOutWebHookBot.Domain.Executors
{
    public class CommandExecutor : ITelegramUpdateListener
    {
        private List<ICommand> commands;

        private ICommand userSendNewsCommand;
        private IListener? listener = null;

        public CommandExecutor()
        {
            commands = new List<ICommand>
            {
                new StartCommand()
                //new GetPartnersCommand(),
                //new AboutAirPowerCommand(),
                //new AboutStudAirPowerCommand()
            };

            userSendNewsCommand = new UserSendNewsCommand(this);
        }

        public async Task GetUpdate(Update update)
        {
            if (listener == null)
            {
                await ExecuteCommand(update);
            }
            else
            {
                await listener.GetUpdate(update);
            }
        }

        private async Task ExecuteCommand(Update update)
        {
            Message msg = update.Message;
            foreach (var command in commands)
            {
                if (command.Name == msg.Text)
                {
                    await command.Execute(update);
                    return;
                }
            }

            await userSendNewsCommand.Execute(update);

        }

        public void StartListen(IListener newListener)
        {
            listener = newListener;
        }

        public void StopListen()
        {
            listener = null;
        }
    }
}
