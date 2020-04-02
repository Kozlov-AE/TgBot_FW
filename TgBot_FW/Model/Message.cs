using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model
{
    public class Message
    {
        public static ObservableCollection<Message> Messages { get; set; }

        public int MessageTgId { get; set; }
        public long ChatID { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public long UserId { get; set; }

        /// <summary>
        /// Пустой конструктор
        /// </summary>
        public Message()
        { }

        public Message(Telegram.Bot.Types.Message message, User user)
        {
            MessageTgId = message.MessageId;
            ChatID = message.Chat.Id;
            Date = message.Date.ToLocalTime();
            Text = message.Text;
            UserId = user.Id;
        }
    }
}
