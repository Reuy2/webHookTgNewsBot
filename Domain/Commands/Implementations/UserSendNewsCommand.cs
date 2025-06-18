using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using WorkOutWebHookBot.BotStrings.Answers;
using WorkOutWebHookBot.Domain.Commands.Interfaces;
using WorkOutWebHookBot.Domain.Executors;
using WorkOutWebHookBot.Domain.Interfaces;

namespace WorkOutWebHookBot.Domain.Commands.Implementations
{
    public class UserSendNewsCommand : ICommand, IListener
    {
        public TelegramBotClient Client => Bot.GetTelegramBot();

        public string Name => "";

        public CommandExecutor Executor { get; }

        List<IAlbumInputMedia> _album = new List<IAlbumInputMedia>();

        private string mediaGroupId;

        private MessageType messageType;

        private bool isManyMediaGroupItems = false;

        private bool isInvalidTextLength = false;

        private bool isMoreThan1Caption = false;

        private static long adminGroupId = -1002106443889;

        private string hashTag = "#Воркаут";

        private string caption = "";

        private MessageEntity[]? entities;

        private int maxTextLength = 0;

        public UserSendNewsCommand(CommandExecutor exec)
        {
            Executor = exec;
        }

        public async Task Execute(Update update)
        {
            try
            {
                if (update.Message.Type == MessageType.Text)
                {
                    var textToCopy = $"{update.Message.Text}\n{hashTag}\n@{update.Message.From.Username}";
                    await Client.SendTextMessageAsync(
                        adminGroupId,
                        textToCopy,
                        entities: update.Message.Entities
                        );
                    await Client.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        BaseAnswers.SuccessToSendNews
                        );
                    return;
                }

                if (update.Message.Type == MessageType.Photo || update.Message.Type == MessageType.Video || update.Message.Type == MessageType.Document)
                {
                    if (!String.IsNullOrEmpty(update.Message.Caption))
                    {
                        var textToCopy1 = $"{update.Message.Caption}\n{hashTag}\n@{update.Message.From.Username}";

                        if (textToCopy1.Length > 1024)
                        {
                            this.isInvalidTextLength = true;
                            maxTextLength = textToCopy1.Length;
                        }

                        if (!String.IsNullOrEmpty(this.caption))
                        {
                            isMoreThan1Caption = true;
                        }

                        this.caption = update.Message.Caption;
                        this.entities = update.Message?.CaptionEntities?.Where(x => x.Type != 0).ToArray();
                    }

                    if (update.Message.Type == MessageType.Photo)
                    {
                        _album.Add(new InputMediaPhoto(new InputMedia(update.Message.Photo[0].FileId)));
                    }
                    else if (update.Message.Type == MessageType.Video)
                    {
                        _album.Add(new InputMediaVideo(new InputMedia(update.Message.Video.FileId)));
                    }
                    else if (update.Message.Type == MessageType.Document)
                    {
                        _album.Add(new InputMediaDocument(new InputMedia(update.Message.Document.FileId)));
                    }

                    if (update.Message.MediaGroupId is not null)
                    {
                        this.mediaGroupId = update.Message.MediaGroupId;
                        this.messageType = update.Message.Type;

                        Executor.StartListen(this);

                        await Client.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            "Подождите ваша новость обрабатывается"
                            );

                        await Task.Delay(5000);

                        Executor.StopListen();
                    }

                    var textToCopy = $"{this.caption}\n{hashTag}\n@{update.Message.From.Username}";

                    if (update.Message.Type == MessageType.Photo)
                    {
                        _album[0] = new InputMediaPhoto(_album[0].Media) { CaptionEntities = this.entities, Caption = textToCopy };
                    }
                    else if (update.Message.Type == MessageType.Video)
                    {
                        _album[0] = new InputMediaVideo(_album[0].Media) { CaptionEntities = this.entities, Caption = textToCopy };
                    }
                    else if (update.Message.Type == MessageType.Document)
                    {
                        _album[0] = new InputMediaDocument(_album[0].Media) { CaptionEntities = this.entities, Caption = textToCopy };
                    }
                    if (isMoreThan1Caption)
                    {
                        await Client.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            BaseAnswers.TooManyCaptions
                            );

                        isMoreThan1Caption = false;
                    }
                    else if (!isInvalidTextLength)
                    {
                        await Client.SendMediaGroupAsync(
                            adminGroupId,
                            _album
                            );
                    }
                    else
                    {
                        await Client.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            $"Ваш текст к новости слишком длинный. Он превышает лимит по символам на {this.maxTextLength - 1024}"
                            );

                        _album = new List<IAlbumInputMedia>();
                        this.caption = "";
                        this.entities = null;
                        isInvalidTextLength = false;
                        isMoreThan1Caption = false;
                        return;
                    }

                    _album = new List<IAlbumInputMedia>();
                    this.caption = "";
                    this.entities = null;
                    isInvalidTextLength = false;
                    isMoreThan1Caption = false;

                    if (isManyMediaGroupItems)
                    {
                        await Client.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            BaseAnswers.TooManyMediaGroupItems
                            );
                        isManyMediaGroupItems = false;
                    }
                    else
                    {
                        await Client.SendTextMessageAsync(
                            update.Message.Chat.Id,
                            BaseAnswers.SuccessToSendNews
                            );
                    }
                }
                else
                {
                    await Client.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        "Мы не принимаем такой тип сообщений)"
                        );
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception(ex.Message);
            }

        }

        public async Task GetUpdate(Update update)
        {
            try
            {
                if (update.Message.Type != this.messageType || update.Message.MediaGroupId != this.mediaGroupId)
                {
                    await Client.SendTextMessageAsync(
                        update.Message.Chat.Id,
                        BaseAnswers.IncorrectUpdateType
                        );
                    return;
                }

                if (_album.Count() == 10)
                {
                    this.isManyMediaGroupItems = true;
                    return;

                }

                if (!String.IsNullOrEmpty(update.Message.Caption))
                {
                    var textToCopy = $"{update.Message.Caption}\n{hashTag}\n@{update.Message.From.Username}";

                    if (textToCopy.Length > 1024)
                    {
                        maxTextLength = textToCopy.Length;
                        this.isInvalidTextLength = true;
                        return;
                    }

                    if (!String.IsNullOrEmpty(this.caption))
                    {
                        isMoreThan1Caption = true;
                    }

                    this.caption = update.Message.Caption;
                    this.entities = update.Message?.CaptionEntities?.Where(x => x.Type != 0).ToArray();
                }

                if (update.Message.Type == MessageType.Photo)
                {
                    _album.Add(new InputMediaPhoto(new InputMedia(update.Message.Photo[0].FileId)));
                    return;
                }

                if (update.Message.Type == MessageType.Video)
                {
                    _album.Add(new InputMediaVideo(new InputMedia(update.Message.Video.FileId)));
                    return;
                }

                if (update.Message.Type == MessageType.Document)
                {
                    _album.Add(new InputMediaDocument(new InputMedia(update.Message.Document.FileId)));
                    return;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           
        }


    }
}
