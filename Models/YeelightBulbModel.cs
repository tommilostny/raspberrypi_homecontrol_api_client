using Newtonsoft.Json;

namespace raspberrypi_homecontrol_api_client.Models
{
    public class YeelightBulbModel
    {
        public string Power { get; set; }

        [JsonProperty("bright")]
        public int Brightness { get; set; }

        [JsonProperty("ct")]
        public int Temperature { get; set; }
    }
}