using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Components.Temperature
{
    public partial class TemperatureCard : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private TemperatureModel Temperature { get; set; }

        private async Task GetTemperatureAsync()
        {
            var response = await httpClient.GetAsync("temperature");
            var content = await response.Content.ReadAsStringAsync();
            Temperature = JsonConvert.DeserializeObject<TemperatureModel>(content);
        }

        protected override async Task OnInitializedAsync()
        {
            await GetTemperatureAsync();
            await base.OnInitializedAsync();
        }
    }
}