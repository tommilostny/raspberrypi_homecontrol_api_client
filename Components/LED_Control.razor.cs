using System;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Net.Http;
using raspberrypi_homecontrol_api_client.Models;

namespace raspberrypi_homecontrol_api_client.Components
{
    public partial class LED_Control
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public LED_Model Led { get; set; }

        private double Interval { get; set; } = 1.0;

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
    }
}