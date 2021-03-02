using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
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

        private void ToggleAllLeds() => Leds.ForEach(async l => await l.ToggleAsync(httpClient));

        private void BlinkAllLeds() => Leds.ForEach(async l => await l.BlinkAsync(httpClient));

        protected override async Task OnInitializedAsync()
        {
            await GetLedsStatus();
            await base.OnInitializedAsync();
        }
    }
}