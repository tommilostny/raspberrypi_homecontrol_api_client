using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;

namespace RaspberryPiHomeControlApiClient.Models
{
    public class LED_Model
    {
        public List<int> Pins { get; set; }

        public bool IsActive { get; set; }

        public int Number { get; set; }

        public bool IsRGB { get; set; }

        public List<float> Color { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public double BlinkInterval { get; set; } = 1.0;

        public async Task Toggle(HttpClient httpClient)
            => await httpClient.GetAsync($"led/{Number}/{((IsActive = !IsActive) ? "on" : "off")}");

        public async Task Blink(HttpClient httpClient)
        {
            await httpClient.GetAsync($"led/{Number}/blink/{string.Format("{0:N2}", BlinkInterval)}");
            IsActive = true;
        }
    }
}