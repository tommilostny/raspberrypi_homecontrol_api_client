using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using raspberrypi_homecontrol_api_client.Models;

namespace raspberrypi_homecontrol_api_client.Components
{
    public partial class YeelightColorMatrix : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public EventCallback RefreshEvent { get; set; }

        [Parameter]
        public ButtonRGB Color { get; set; }
        
        private List<ButtonRGB> AllButtonsRGB { get; set; } = new List<ButtonRGB>();

        private string SetButtonColor(ButtonRGB color) => $"background-color: rgb({color.Red},{color.Green},{color.Blue});"; 

        private async Task YeelightColor(ButtonRGB color)
        {
            await httpClient.GetAsync($"yeelight/color/{color.Red}/{color.Green}/{color.Blue}");
            await RefreshEvent.InvokeAsync();
        }

        protected override Task OnInitializedAsync()
        {
            /* 255/51=5 <-> Generate 5*5*5=125 colors to choose from */
            for (int r = 0; r <= 255; r += 51)
            {
                for (int g = 0; g <= 255; g += 51)
                {
                    for (int b = 0; b <= 255; b += 51)
                    {
                        if (r != 0 || g != 0 || b != 0)
                            AllButtonsRGB.Add(new ButtonRGB { Red = r, Green = g, Blue = b });
                    }
                }   
            }
            return base.OnInitializedAsync();
        }
    }
}