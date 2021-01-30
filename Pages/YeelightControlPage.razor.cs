using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;
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

        private YeelightColorMatrix colorMatrix { get; set; }

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
            var response = await httpClient.GetAsync($"yeelight");
            var content = await response.Content.ReadAsStringAsync();
            Bulb = JsonConvert.DeserializeObject<YeelightBulbModel>(content);
            DecodeColor();
        }

        private void DecodeColor()
        {
            Bulb.Color = new ButtonRGB
            {
                Blue = Bulb.RGB & 0x000000FF,
                Green = (Bulb.RGB & 0x0000FF00) >> 8,
                Red = (Bulb.RGB & 0x00FF0000) >> 16
            };
        }

        protected override async Task OnInitializedAsync()
        {
            await GetBulbStatus();
            await base.OnInitializedAsync();
        }
    }
}