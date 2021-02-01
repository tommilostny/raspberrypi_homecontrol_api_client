using Newtonsoft.Json;

namespace RaspberryPiHomeControlApiClient.Models
{
    public class TemperatureModel
    {
        [JsonProperty("tempC")]
        public float Celsius { get; set; }

        [JsonProperty("tempF")]
        public float Fahrenheit { get; set; }

        [JsonProperty("tempK")]
        public float Kelvin { get; set; }
    }
}