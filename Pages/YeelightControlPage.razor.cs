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

        private async Task YeelightToggle()
        {
            await httpClient.GetAsync($"yeelight/power/toggle");
        }
    }
}