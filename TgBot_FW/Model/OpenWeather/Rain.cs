using Newtonsoft.Json;

namespace TgBot_FW.OpenWeather
{
    public class Rain
    {
        [JsonProperty("3h")]
        public double The3H { get; set; }
    }
}