using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model
{
    public class User : INotifyBase
    {
        #region Поля
        long id;
        /// <summary>
        /// Id пользователя
        /// </summary>
        public long Id
        {
            get => id;
            set
            {
                if (id != value)
                {
                    id = value;
                    OnPropertyChanged();
                }
            }
        }

        string firstName;
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get => firstName;
            set
            {
                if (firstName != value)
                {
                    firstName = value;
                    OnPropertyChanged();
                }
            }
        }

        string lastName;
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get => lastName;
            set
            {
                if (lastName != value)
                {
                    lastName = value;
                    OnPropertyChanged();
                }
            }
        }

        long chatId;
        /// <summary>
        /// идентификатор чата из которого получено последнее сообщение
        /// </summary>
        public long ChatId
        {
            get => chatId;
            set
            {
                if (chatId != value)
                {
                    chatId = value;
                    OnPropertyChanged();
                }
            }
        }
        #endregion
        static public ObservableCollection<User> Users { get; set; }
        public User(long id)
        {
            this.Id = id;
        }

        /// <summary>
        /// Получение пользвателя из списка по Id
        /// </summary>
        public static User GetUserFromCollection(ObservableCollection<User> users, long id)
        {
            return User.Users.FirstOrDefault(u => u.Id == id);
        }

        [JsonIgnore]
        public string PrintUser => this.Id + " " + this.firstName;


        /// <summary>
        /// Перегрузка сравнения под мой класс
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;
            User user = obj as User;
            if (user as User == null)
                return false;
            return user.Id == Id;
        }
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

    }
}
