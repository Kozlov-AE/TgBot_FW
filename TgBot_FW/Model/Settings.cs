using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TgBot_FW.Model
{
    public class Settings
    {
        static public string BasePath => @"./base/";
        public string TelegrammToken;       // Токен для бота
        public string JsoneFilePath;        // Токен для ДиалогФлоу
        public string DialogFlowProject;    // Название проекта DialogFlow
        public string OpenWeatherToken;     // Токен для OpenWeather

        public string ErrorMessage = "Сообщение об ошибке";

        public Settings(){}

        /// <summary>
        /// Сериализовать в json
        /// </summary>
        /// <param name="path">путь к файлу</param>
        public void SerializeToJson(string path)
        {
            string json = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(path, json);
        }
        /// <summary>
        /// Десереализуем настройки из файла
        /// </summary>
        static public bool DeserealizeFromJsone(string path, out Settings settings)
        {
            settings = new Settings();
            if (File.Exists(path))
            {
                try
                {
                    settings = JsonConvert.DeserializeObject<Settings>(path);
                    return true;
                }
                catch (Exception)
                {
                    return false;
                }
            }
            return false;
        }

        public bool CheckAllFields()
        {
            return  !string.IsNullOrWhiteSpace(TelegrammToken) &&
                    !string.IsNullOrWhiteSpace(JsoneFilePath) &&
                    !string.IsNullOrWhiteSpace(DialogFlowProject) &&
                    !string.IsNullOrWhiteSpace(OpenWeatherToken) &&
                    !string.IsNullOrWhiteSpace(ErrorMessage);
        }
    }
}
