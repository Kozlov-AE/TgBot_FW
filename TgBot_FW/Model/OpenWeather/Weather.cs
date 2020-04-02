using Newtonsoft.Json;
using System.Drawing;

namespace TgBot_FW.OpenWeather
{
    public class Weather
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("main")]
        public string Main { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        //[JsonProperty("icon")]
        public string icon;
        public Bitmap Icon => new Bitmap(Image.FromFile($"Icons/{icon}.png"));
    }
}