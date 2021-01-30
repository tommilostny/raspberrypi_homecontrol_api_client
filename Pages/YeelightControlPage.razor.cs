using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using raspberrypi_homecontrol_api_client.Models;

namespace raspberrypi_homecontrol_api_client.Pages
{
    public partial class YeelightControlPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private YeelightBulbModel Bulb { get; set; }

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

        public async Task GetBulbStatus()
        {
            var response = await httpClient.GetAsync($"yeelight");
            var content = await response.Content.ReadAsStringAsync();
            Bulb = JsonConvert.DeserializeObject<YeelightBulbModel>(content);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetBulbStatus();
            await base.OnInitializedAsync();
        }
    }
}