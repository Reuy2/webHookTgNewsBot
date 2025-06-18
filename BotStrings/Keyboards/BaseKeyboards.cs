using Telegram.Bot.Types.ReplyMarkups;
using WorkOutWebHookBot.BotStrings.Commands;

namespace WorkOutWebHookBot.BotStrings.Keyboards
{
    public static class BaseKeyboards
    {
        public static IReplyMarkup GetStartButtons()
        {
            IReplyMarkup keyboard = new ReplyKeyboardMarkup(
                                        new KeyboardButton[][]
                                        {
                                            new KeyboardButton[] {new KeyboardButton(BaseCommands.AirPowerAbout) },
                                            new KeyboardButton[] {new KeyboardButton(BaseCommands.StudAirPowerAbout) },
                                            new KeyboardButton[] {new KeyboardButton(BaseCommands.Partners) }
                                        })
            { ResizeKeyboard = true };
            return keyboard;
        }
    }
}
