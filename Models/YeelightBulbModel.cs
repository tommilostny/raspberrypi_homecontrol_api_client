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

        public int RGB { get; set; }

        [JsonIgnore]
        public ButtonRGB Color { get; set; }

        public int Hue { get; set; }

        [JsonProperty("sat")]
        public int Saturation { get; set; }
    }

    public class ButtonRGB
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }
    }
}