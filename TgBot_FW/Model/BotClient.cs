using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.Enums;

namespace TgBot_FW.Model
{
    class BotClient : INotifyBase
    {
        TelegramBotClient bot;
        Settings settings;

        bool isworking;
        User Iam;
        static object locker = new object();
        Dispatcher dispatcher;

        public bool IsWorking
        {
            get => isworking;
            set
            {
                if (isworking != value)
                {
                    isworking = value;
                    OnPropertyChanged();
                }
            }
        }

        public BotClient(Settings settings, Dispatcher dispatcher)
        {
            this.settings = settings;
            bot = new TelegramBotClient(settings.TelegrammToken);
            IsWorking = false;
            this.dispatcher = dispatcher;
        }

        /// <summary>
        /// Запуск работы бота
        /// </summary>
        public async void StartClient()
        {
            if (bot.IsReceiving) return;
            bot.OnMessage += OnMessage;
            bot.StartReceiving();
            var Bot = await bot.GetMeAsync();
            Iam = new User(Bot.Id);
            if (!User.Users.Contains(Iam))
            {
                Iam.FirstName = Bot.FirstName;
                Iam.LastName = Bot.LastName;
                dispatcher.Invoke(() =>
                {
                    User.Users.Add(Iam);
                });
            }
            else
            {
                Iam = User.GetUserFromCollection(User.Users, Bot.Id);
            }
            IsWorking = true;
        }

        /// <summary>
        /// Остановка бота
        /// </summary>
        public void StopClient()
        {
            if (bot.IsReceiving) bot.StopReceiving();
            IsWorking = false;
        }
        /// <summary>
        /// Обработка входящих сообщений
        /// </summary>
        private async void OnMessage(object sender, MessageEventArgs e)
        {
            var us = e.Message.From;
            User user = new User(us.Id);

            if (!User.Users.Contains(user))
            {
                user.FirstName = us.FirstName;
                user.LastName = us.LastName;
                user.ChatId = e.Message.Chat.Id;
                dispatcher.Invoke(() =>
                {
                    User.Users.Add(user);
                });
            }
            else
            {
                user = User.GetUserFromCollection(User.Users, user.Id);
            }
            switch (e.Message.Type)
            {
                case MessageType.Text:
                    AddMessage(new Message(e.Message, user), dispatcher);
                    string answer = "";
                    if (e.Message.Text[0] == '/')
                    {
                        answer = new TgCommandsHandler(e.Message.Text).GetResponse();
                        SendMessage(user.ChatId, answer);
                    }
                    else
                    {
                        var nch = new NoCommandHandler(user.Id.ToString(),settings);
                        answer = await nch.AnswerToMessage(e.Message.Text);
                        if (answer != null) SendMessage(user.ChatId, answer);
                        else
                        {
                            answer = settings.ErrorMessage;
                            SendMessage(user.ChatId, answer);
                        }
                    }
                    Message m = new Message();
                    m.ChatID = user.ChatId;
                    m.Date = e.Message.Date;
                    m.Text = answer;
                    m.UserId = Iam.Id;
                    AddMessage(m, dispatcher);
                    //answer = null;
                    break;
                case MessageType.Photo:
                    break;
                case MessageType.Document:
                    break;
            }
        }
        /// <summary>
        /// Отправить сообщение от имени бота
        /// </summary>
        public async void SendMessage(long chatId, string text)
        {
            await bot.SendTextMessageAsync(chatId, text);
        }

        /// <summary>
        /// Добавляет сообщение в коллекцию сообщений
        /// </summary>
        void AddMessage(Message m, Dispatcher d)
        {
            d.Invoke(() =>
            {
                Message.Messages.Add(m);
            });
        }
        /// <summary>
        /// Отправить файл
        /// </summary>
        /// <param name="fi"></param>
        /// <param name="chatId"></param>
        //public void SendFile(string filePath, User user, string message = "")
        //{
        //    lock (locker)
        //    {
        //        using (var stream = File.OpenRead(filePath))
        //        {
        //            bot.SendDocumentAsync(user.GetChatId(), new InputOnlineFile(stream), message);
        //        }
        //    }
        //}
    }
}
