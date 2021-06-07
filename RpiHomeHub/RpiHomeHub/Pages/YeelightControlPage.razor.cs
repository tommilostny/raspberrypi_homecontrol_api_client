using Microsoft.AspNetCore.Components;
using RpiHomeHub.Models;
using RpiHomeHub.Services;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RpiHomeHub.Pages
{
    public partial class YeelightControlPage : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Inject]
        private YeelightBulbService yeelightService { get; set; }

        private YeelightBulbModel Bulb { get; set; }

        //private ErrorDialog BadResponseDialog { get; set; }


        private async Task YeelightToggle()
        {
            try
            {
                Bulb = await yeelightService.Toggle();
            }
            catch
            {
                //BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightBrightness(ChangeEventArgs e)
        {
            try
            {
                var brightness = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"lights/brightness/{brightness}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //await BadResponseDialog.Show(response);
                }
                else Bulb.Brightness = brightness;
            }
            catch
            {
                //BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightTemperature(ChangeEventArgs e)
        {
            try
            {
                var temperature = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"lights/temperature/{temperature}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //await BadResponseDialog.Show(response);
                }
                else Bulb.Temperature = temperature;
            }
            catch
            {
                //BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightHue(ChangeEventArgs e)
        {
            try
            {
                var hue = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"lights/hs/{hue}/{Bulb.Saturation}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //await BadResponseDialog.Show(response);
                }
                else Bulb.Hue = hue;
            }
            catch
            {
                //BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightSaturation(ChangeEventArgs e)
        {
            try
            {
                var saturation = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"lights/hs/{Bulb.Hue}/{saturation}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    //await BadResponseDialog.Show(response);
                }
                else Bulb.Saturation = saturation;
            }
            catch
            {
                //BadResponseDialog.Show(exception.Message);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Bulb = await yeelightService.GetStatus();
            await base.OnInitializedAsync();
        }
    }
}
