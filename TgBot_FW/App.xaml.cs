using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TgBot_FW.Model;
using TgBot_FW.View;
using TgBot_FW.ViewModel;

namespace TgBot_FW
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {}

        protected override void OnStartup(StartupEventArgs e)
        {
            ShowMainWindow();
        }

        void ShowMainWindow()
        {
            MainVM vm = new MainVM(Dispatcher);
            MainView mv = new MainView()
            {
                DataContext = vm
            };
            mv.Show();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            try
            {
                new JsonWorker().SaveUsers(User.Users, Settings.BasePath + "Users.json");
                new JsonWorker().SaveMessages(Message.Messages, Settings.BasePath + "Messages.json");
            }
            finally
            {
                base.OnExit(e);
            }
        }
    }
}
