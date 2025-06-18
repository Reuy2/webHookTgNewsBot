using Telegram.Bot;
using Telegram.Bot.Types;
using WorkOutWebHookBot.BotStrings.Answers;
using WorkOutWebHookBot.BotStrings.Commands;
using WorkOutWebHookBot.BotStrings.Keyboards;
using WorkOutWebHookBot.Domain.Commands.Interfaces;
using WorkOutWebHookBot.Services.Implementations;
using WorkOutWebHookBot.Services.Interfaces;

namespace WorkOutWebHookBot.Domain.Commands.Implementations
{
    public class GetPartnersCommand : ICommand
    {

        private readonly IPartnerService _partnerService = new PartnerService();

        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name => BaseCommands.Partners;

        public async Task Execute(Update update)
        {
            var partnersResponse = await _partnerService.GetPartners();

            if(partnersResponse.StatusCode != Enums.StatusCode.Ok)
            {
                await Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    BaseAnswers.Error
                    );
                return;
            }

            var partners = partnersResponse.Data;

            if(partners is null || partners.Count() == 0)
            {
                await Client.SendTextMessageAsync(
                    update.Message.Chat.Id,
                    BaseAnswers.ZeroPartners,
                    replyMarkup: BaseKeyboards.GetStartButtons()
                    );
                return;
            }

            foreach(var partner in partners)
            {
                await Client.SendPhotoAsync(
                    update.Message.Chat.Id,
                    new Telegram.Bot.Types.InputFiles.InputOnlineFile(partner.Url),
                    partner.Caption
                    );
            }
        }
    }
}
