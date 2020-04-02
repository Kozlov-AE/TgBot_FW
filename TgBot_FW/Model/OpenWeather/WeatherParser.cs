using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TgBot_FW.OpenWeather
{
    public class WeatherParser
    {
        string apiKey;
        string city;
        public OpenWeather OpenWeather { get; set; }

        public WeatherParser(string city, string apiKey)
        {
            this.apiKey = apiKey;
            this.city = city;
        }


        public async Task<OpenWeather> GetRequest()
        {
            WebRequest request = WebRequest.Create($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={apiKey}");
            request.Method = "POST";
            request.ContentType = "Applicaion/x-www-urlencoded";

            WebResponse response = await request.GetResponseAsync();
            string answer = string.Empty;

            using (Stream s = response.GetResponseStream())
            {
                using (StreamReader reader = new StreamReader(s))
                {
                    answer = await reader.ReadToEndAsync();
                }
            }
            response.Close();
            
            return JsonConvert.DeserializeObject<OpenWeather>(answer);
        }

        string GetTemperature()
        {
            return OpenWeather.Main.Temp.ToString();
        }
    }
}
