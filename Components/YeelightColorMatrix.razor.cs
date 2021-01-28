using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;

namespace raspberrypi_homecontrol_api_client.Components
{
    public partial class YeelightColorMatrix
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private class ButtonRGB
        {
            public int Red { get; set; }
            public int Green { get; set; }
            public int Blue { get; set; }
        }
        
        private List<ButtonRGB> allButtonsRGB = new List<ButtonRGB>();

        private string selectedColor = string.Empty;

        private string SetButtonColor(ButtonRGB color) => $"background-color: rgb({color.Red},{color.Green},{color.Blue});"; 

        private async Task YeelightColor(ButtonRGB color)
        {
            await httpClient.GetAsync($"yeelight/color/{color.Red}/{color.Green}/{color.Blue}");
            selectedColor = $"rgb({color.Red}, {color.Green}, {color.Blue})";
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
                            allButtonsRGB.Add(new ButtonRGB { Red = r, Green = g, Blue = b });
                    }
                }   
            }
            return base.OnInitializedAsync();
        }
    }
}