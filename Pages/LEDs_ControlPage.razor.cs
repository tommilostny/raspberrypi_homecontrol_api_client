using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Pages
{
    public partial class LEDs_ControlPage
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

        private async Task ToggleAllLeds() => await InvokeAllLeds(led => led.Toggle(httpClient));

        private async Task BlinkAllLeds() => await InvokeAllLeds(led => led.Blink(httpClient));

        private async Task InvokeAllLeds(Func<LED_Model, Task> action)
        {
            var tasks = new List<Task>();
            foreach (var led in Leds)
            {
                tasks.Add(action(led));
            }
            await Task.WhenAll(tasks);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetLedsStatus();
            await base.OnInitializedAsync();
        }
    }
}