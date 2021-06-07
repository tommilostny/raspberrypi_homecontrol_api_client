using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using RpiHomeHub.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace RpiHomeHub.Components.Temperature
{
    public partial class TemperatureCard : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private TemperatureModel Temperature { get; set; }

        private async Task GetTemperature()
        {
            var response = await httpClient.GetAsync("temperature");
            var content = await response.Content.ReadAsStringAsync();
            Temperature = JsonConvert.DeserializeObject<TemperatureModel>(content);
        }

        private async Task SetTempThreshold(string period)
        {
            float newThreshold = period switch
            {
                "day" => Temperature.ThresholdDay,
                _ => Temperature.ThresholdNight
            };
            await httpClient.GetAsync($"temp_threshold/{period}/{string.Format("{0:N3}", newThreshold)}");
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTemperature();
            await base.OnInitializedAsync();
        }
    }
}
