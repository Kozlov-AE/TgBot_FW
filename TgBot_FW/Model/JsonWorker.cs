using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model
{
    class JsonWorker
    {
        public void SaveUsers(ObservableCollection<User> users, string path)
        {
            string json = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public void SaveMessages(ObservableCollection<Message> messages, string path)
        {
            string json = JsonConvert.SerializeObject(messages, Formatting.Indented);
            File.WriteAllText(path, json);
        }

        public ObservableCollection<User> LoadUsers ()
        {
            string pathToFile = Settings.BasePath + "Users.json";
            ObservableCollection<User> users = new ObservableCollection<User>();
            if (File.Exists(pathToFile))
            {
                //string str = File.ReadAllText(pathToFile);
                users = JsonConvert.DeserializeObject<ObservableCollection<User>>(File.ReadAllText(pathToFile));
            }
            return users;
        }

        public ObservableCollection<Message> LoadMessages()
        {
            string pathToFile = Settings.BasePath + "Messages.json";
            ObservableCollection<Message> messages = new ObservableCollection<Message>();
            if (File.Exists(pathToFile))
                messages = JsonConvert.DeserializeObject<ObservableCollection<Message>>(File.ReadAllText(pathToFile));
            return messages;
        }

    }
}
