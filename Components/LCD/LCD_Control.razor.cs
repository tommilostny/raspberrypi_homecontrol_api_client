using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;

namespace RaspberryPiHomeControlApiClient.Components.LCD
{
    public partial class LCD_Control : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private string Message { get; set; } = string.Empty;


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

        private async Task LcdBacklightToggle() => await httpClient.GetAsync("heater_lcd/2");
    }
}