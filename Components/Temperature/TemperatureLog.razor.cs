using Newtonsoft.Json;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using System.Net;

namespace RaspberryPiHomeControlApiClient.Components.Temperature
{
    public partial class TemperatureLog : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private List<string> LogLines { get; set; }

        private async Task FetchLog()
        {
            var response = await httpClient.GetAsync("temp_log");
            var content = await response.Content.ReadAsStringAsync();
            LogLines = JsonConvert.DeserializeObject<List<string>>(content);
            LogLines.Reverse();
        }

        private async Task ClearLog()
        {
            
            var response = await httpClient.GetAsync("temp_log/clear");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                LogLines = new List<string>();
            }
            //else ErrorDialog?
        }

        protected override async Task OnInitializedAsync()
        {
            await FetchLog();
            await base.OnInitializedAsync();
        }
    }
}