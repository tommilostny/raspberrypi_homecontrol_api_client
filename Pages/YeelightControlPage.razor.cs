using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using raspberrypi_homecontrol_api_client.Models;
using raspberrypi_homecontrol_api_client.Components;

namespace raspberrypi_homecontrol_api_client.Pages
{
    public partial class YeelightControlPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private YeelightBulbModel Bulb { get; set; }

        private ColorPicker colorMatrix { get; set; }

        private async Task YeelightToggle()
        {
            await httpClient.GetAsync("yeelight/power/toggle");
            await GetBulbStatus();
        }

        private async Task YeelightBrightness(ChangeEventArgs e)
        {
            Bulb.Brightness = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/brightness/{Bulb.Brightness}");
        }

        private async Task YeelightTemperature(ChangeEventArgs e)
        {
            Bulb.Temperature = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/temperature/{Bulb.Temperature}");
        }

        private async Task YeelightHue(ChangeEventArgs e)
        {
            Bulb.Hue = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/hs/{Bulb.Hue}/{Bulb.Saturation}");
        }

        private async Task YeelightSaturation(ChangeEventArgs e)
        {
            Bulb.Saturation = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/hs/{Bulb.Hue}/{Bulb.Saturation}");
        }

        private async Task GetBulbStatus()
        {
            var response = await httpClient.GetAsync("yeelight");
            var content = await response.Content.ReadAsStringAsync();
            Bulb = JsonConvert.DeserializeObject<YeelightBulbModel>(content);
            Bulb.Color = DecodeColor(Bulb.RGB);
        }

        private ColorRGB DecodeColor(int rgb) => new ColorRGB
        {
            Blue = rgb & 0x0000FF,
            Green = (rgb & 0x00FF00) >> 8,
            Red = (rgb & 0xFF0000) >> 16
        };

        protected override async Task OnInitializedAsync()
        {
            await GetBulbStatus();
            await base.OnInitializedAsync();
        }
    }
}