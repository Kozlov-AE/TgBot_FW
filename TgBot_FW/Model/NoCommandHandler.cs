using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TgBot_FW.Model.DialogFlow;
using TgBot_FW.OpenWeather;

namespace TgBot_FW.Model
{
    class NoCommandHandler
    {
        Settings settings;
        //string inputString;
        string userId;

        public NoCommandHandler(Settings settings)
        {
            this.settings = settings;
        }
        public NoCommandHandler(string userId, Settings settings)
        {
            this.userId = userId;
            this.settings = settings;
        }

        public async Task<string> AnswerToMessage(string message)
        {
            DialogflowManager dfm = new DialogflowManager(userId, settings);
            DialogFlow.ResponseHandler responseHandler = new DialogFlow.ResponseHandler(await dfm.CheckIntent(message));
            string result = "";

            if (responseHandler.Messages != null)
            {
                foreach (string s in responseHandler.Messages)
                    result += s + "\n";
            }
            else result = null;
            if (responseHandler.Context.Contains("weather") && ((responseHandler.Parameters != null) && (responseHandler.Parameters.ContainsKey("city"))))
            {
                string city;
                if (responseHandler.Parameters.TryGetValue("city", out city))
                {
                    OpenWeather.OpenWeather ow = new OpenWeather.OpenWeather();
                    var wp = new WeatherParser(city, settings.OpenWeatherToken);
                    ow = await wp.GetRequest();
                    result = ow.Main.Temp.ToString("0.##");
                }
            }
            return result;
        }

    }
}
