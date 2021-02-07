using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using Newtonsoft.Json;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Components
{
    public partial class ColorPicker : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public EventCallback RefreshEvent { get; set; }

        [Parameter]
        public ColorRGB Color { get; set; }

        [Parameter]
        public ColorMode Mode { get; set; }

        [Parameter]
        public bool ShowLabels { get; set; } = true;
        
        private List<ColorModel> Colors { get; set; }

        private ColorRGB CustomColor { get; set; }


        private string SetButtonColor(ColorRGB color) => $"background-color: rgb({color.Red},{color.Green},{color.Blue});"; 

        private async Task SendColor(ColorRGB color)
        {
            switch (Mode)
            {
                case ColorMode.Yeelight:
                    await httpClient.GetAsync($"yeelight/color/{color.Red}/{color.Green}/{color.Blue}");
                    break;
                case ColorMode.LED:
                    await httpClient.GetAsync($"led/rgb/{color.Red}/{color.Green}/{color.Blue}");
                    break;
            }
            await RefreshEvent.InvokeAsync();
            Color = color;
        }

        protected override async Task OnInitializedAsync()
        {
            var response = await httpClient.GetAsync($"colors");
            var content = await response.Content.ReadAsStringAsync();
            Colors = JsonConvert.DeserializeObject<List<ColorModel>>(content);

            CustomColor = new ColorRGB
            {
                Red = Color.Red, Green = Color.Green, Blue = Color.Blue
            };

            await base.OnInitializedAsync();
        }

        private string GetButtonDimension() => Mode == ColorMode.Yeelight ? "64px;" : "32px;";
    }
}