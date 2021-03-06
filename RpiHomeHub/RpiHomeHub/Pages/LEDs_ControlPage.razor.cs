﻿using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RpiHomeHub.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace RpiHomeHub.Pages
{
    public partial class LEDs_ControlPage : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private List<LED_Model> Leds { get; set; }

        private async Task GetLedsStatus()
        {
            var response = await httpClient.GetAsync("led");
            var content = await response.Content.ReadAsStringAsync();
            Leds = JsonConvert.DeserializeObject<List<LED_Model>>(content);
        }

        private void ToggleAllLeds() => Leds.ForEach(async l => await l.ToggleAsync(httpClient));

        private void BlinkAllLeds() => Leds.ForEach(async l => await l.BlinkAsync(httpClient));

        protected override async Task OnInitializedAsync()
        {
            await GetLedsStatus();
            await base.OnInitializedAsync();
        }
    }
}
