using Newtonsoft.Json;

namespace TgBot_FW.OpenWeather
{
    public class Clouds
    {
        [JsonProperty("all")]
        public long All { get; set; }
    }
}