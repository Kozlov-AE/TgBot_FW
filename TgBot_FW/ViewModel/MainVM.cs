using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using TgBot_FW.Model;
using TgBot_FW.View;

namespace TgBot_FW.ViewModel
{
    public class MainVM
    {
        public Dispatcher Dispatcher { get; set; }
        Model.Settings settings;

        BotClient client;
        public User SelectedUser { get; set; }
        public ObservableCollection<User> Users => User.Users;
        public ObservableCollection<Message> Messages => Message.Messages;


        public MainVM()
        {
            try
            {
            User.Users = new JsonWorker().LoadUsers();
            Message.Messages = new JsonWorker().LoadMessages();
            }
            catch
            {
                User.Users = new ObservableCollection<User>();
                Message.Messages = new ObservableCollection<Message>();
            }
        }
        public MainVM(Dispatcher dispatcher)
            : this()
        {
            Dispatcher = dispatcher;
            settings = LoadSettings();
            client = new BotClient(settings, Dispatcher);
        }

        Settings LoadSettings()
        {
            Settings set = null;
            do
            {
                try
                {
                    string sets = File.ReadAllText("Settings.json");
                    set = JsonConvert.DeserializeObject<Settings>(sets);
                }
                catch
                {
                    SettingsVM vm = new SettingsVM();
                    SettingsView mv = new SettingsView()
                    {
                        DataContext = vm
                    };
                    mv.ShowDialog();
                }
            } while (set == null);
            return set;
        }
        #region Комманды

        #region Главное
        /// <summary>
        /// Запуск работы бота
        /// </summary>
        RelayCommand startBot;
        /// <summary>
        /// Остановка работы бота
        /// </summary>
        RelayCommand stopBot;
        /// <summary>
        /// Выход из приложения
        /// </summary>
        RelayCommand exit;

        public RelayCommand StartBot => startBot ?? (startBot = new RelayCommand(
            o => client.StartClient(),
            o => !client.IsWorking));
        public RelayCommand StoptBot => stopBot ?? (stopBot = new RelayCommand(
            o => client.StopClient(),
            o => client.IsWorking));
        public RelayCommand Exit => exit ?? (exit = new RelayCommand(
            o => System.Windows.Application.Current.Shutdown(),
            o => true));
        #endregion

        #region Сохранить
        RelayCommand saveUsers;
        public RelayCommand SaveUsers => saveUsers ?? (saveUsers = new RelayCommand(
        o =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON документ|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                new JsonWorker().SaveUsers(User.Users, saveFileDialog.FileName);
            }
        },
        o => User.Users.Count > 0));

        RelayCommand saveMessages;
        public RelayCommand SaveMessages => saveMessages ?? (saveMessages = new RelayCommand(
        o =>
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "JSON документ|*.json";
            if (saveFileDialog.ShowDialog() == true)
            {
                new JsonWorker().SaveMessages(Message.Messages, saveFileDialog.FileName);
            }
        },
        o => Message.Messages.Count > 0));
        #endregion

        #region Настройки
        /// <summary>
        /// Вызов окна настроек
        /// </summary>
        RelayCommand opensettings;
        public RelayCommand Opensettings => opensettings ?? (opensettings = new RelayCommand(
        o =>
        {
            SettingsVM vm = new SettingsVM(settings);
            SettingsView mv = new SettingsView()
            {
                DataContext = vm
            };
            if (mv.ShowDialog() == true)
            {
                this.settings = LoadSettings();
            }
        },
        o => true));

        #endregion

        #endregion

    }
}
