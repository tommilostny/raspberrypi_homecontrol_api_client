using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Pages
{
    public partial class Index
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