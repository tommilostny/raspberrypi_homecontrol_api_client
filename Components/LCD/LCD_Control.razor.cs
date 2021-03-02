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

        [Parameter]
        public int LineWidth { get; set; } = 16;

        [Parameter]
        public int LinesCount { get; set; } = 2;

        private int LcdCapacity { get => LineWidth * LinesCount; }

        private async Task SendMessageToLcd()
        {
            var lines = new string[LinesCount];
            for (int line = 0; line < LinesCount; line++)
            {
                lines[line] = string.Empty;
                for (int i = 0; i < LineWidth; i++)
                {
                    int index = i + line * LineWidth;
                    lines[line] += index < Message.Length ? Message[index] : ' ';
                }
                await httpClient.GetAsync($"lcd_message/{lines[line]}/{line + 1}");
            }
        }

        private async Task LcdBacklightToggle() => await httpClient.GetAsync("heater_lcd/2");

        private void MessageInput(ChangeEventArgs e)
        {
            var input = e.Value.ToString();
            Message = input.Length <= LcdCapacity ? input : input.Substring(0, LcdCapacity);
        }
    }
}