using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace RaspberryPiHomeControlApiClient.Pages
{
    public partial class HeaterLcdPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private string Message { get; set; } = string.Empty;

        private List<string> LogLines { get; set; }


        private async Task SendMessageToLcd()
        {
            string line1 = string.Empty;
            string line2 = string.Empty;
            
            for (int i = 0; i < 16; i++)
            {
                line1 += i < Message.Length ? Message[i] : ' ';
                line2 += i + 16 < Message.Length ? Message[i + 16] : ' ';
            }

            await httpClient.GetAsync($"lcd/{line1}/1");
            await httpClient.GetAsync($"lcd/{line2}/2");
        }

        private async Task LcdBacklight(int state) => await httpClient.GetAsync($"heater_lcd/{state}");

        private async Task FetchLog()
        {
            var response = await httpClient.GetAsync("temp_log");
            var content = await response.Content.ReadAsStringAsync();
            LogLines = JsonConvert.DeserializeObject<List<string>>(content);
        }

        private async Task ClearLog()
        {
            await httpClient.GetAsync("temp_log/clear");
            await FetchLog();
        }

        protected override async Task OnInitializedAsync()
        {
            await FetchLog();
            await base.OnInitializedAsync();
        }
    }
}