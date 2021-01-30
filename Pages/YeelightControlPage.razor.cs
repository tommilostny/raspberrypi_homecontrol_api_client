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

        private int Brightness { get; set; } = 100;

        private int Temperature { get; set; } = 4000;

        private async Task YeelightToggle()
        {
            await httpClient.GetAsync("yeelight/power/toggle");
        }

        private async Task YeelightBrightness(ChangeEventArgs e)
        {
            Brightness = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/brightness/{Brightness}");
        }

        private async Task YeelightTemperature(ChangeEventArgs e)
        {
            Temperature = Convert.ToInt32(e.Value);
            await httpClient.GetAsync($"yeelight/temperature/{Temperature}");
        }
    }
}