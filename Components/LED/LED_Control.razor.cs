using System;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Components.LED
{
    public partial class LED_Control : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public LED_Model Led { get; set; }

        private double Interval { get; set; } = 1.0;

        private ColorRGB MappedColor { get; set; } = null;

        private async Task LED_Toggle()
        {
            if (Led.IsActive = !Led.IsActive)
                await httpClient.GetAsync($"led/{Led.Number}/on");
            else
                await httpClient.GetAsync($"led/{Led.Number}/off");
        }

        private async Task LED_Blink()
        {
            await httpClient.GetAsync($"led/{Led.Number}/blink/{string.Format("{0:N2}", Interval)}");
            Led.IsActive = true;
        }

        private void IntervalInput_ChangeEvent(ChangeEventArgs e)
        {
            var inputValue = Convert.ToDouble(e.Value);
            Interval = inputValue > 0 ? inputValue : 1.0;
        }

        private string LedStatusAsString() => Led.IsActive ? "on" : "off";

        private string LedStatusImage() => $"led{Led.Number}{LedStatusAsString()}.jpg";

        protected override async Task OnInitializedAsync()
        {
            if (Led.IsRGB)
            {
                MappedColor = new ColorRGB
                {
                    Red = (int)(Led.Color[0] * 255.0),
                    Green = (int)(Led.Color[1] * 255.0),
                    Blue = (int)(Led.Color[2] * 255.0),
                };
            }
            await base.OnInitializedAsync();
        }
    }
}