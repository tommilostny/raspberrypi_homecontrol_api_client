using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using RaspberryPiHomeControlApiClient.Models;
using RaspberryPiHomeControlApiClient.Components;

namespace RaspberryPiHomeControlApiClient.Pages
{
    public partial class YeelightControlPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private YeelightBulbModel Bulb { get; set; }

        private ErrorDialog BadResponseDialog { get; set; }


        private async Task YeelightToggle()
        {
            try
            {
                await httpClient.GetAsync("yeelight/power/toggle");
                await GetBulbStatus();
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightBrightness(ChangeEventArgs e)
        {
            try
            {
                var brightness = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"yeelight/brightness/{brightness}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await BadResponseDialog.Show(response);
                }
                else Bulb.Brightness = brightness;
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightTemperature(ChangeEventArgs e)
        {
            try
            {
                var temperature = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"yeelight/temperature/{temperature}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await BadResponseDialog.Show(response);
                }
                else Bulb.Temperature = temperature;
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightHue(ChangeEventArgs e)
        {
            try
            {
                var hue = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"yeelight/hs/{hue}/{Bulb.Saturation}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await BadResponseDialog.Show(response);
                }
                else Bulb.Hue = hue;
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task YeelightSaturation(ChangeEventArgs e)
        {
            try
            {
                var saturation = Convert.ToInt32(e.Value);
                var response = await httpClient.GetAsync($"yeelight/hs/{Bulb.Hue}/{saturation}");
                if (response.StatusCode != HttpStatusCode.OK)
                {
                    await BadResponseDialog.Show(response);
                }
                else Bulb.Saturation = saturation;
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private async Task GetBulbStatus()
        {
            try
            {
                var response = await httpClient.GetAsync("yeelight");
                var content = await response.Content.ReadAsStringAsync();
                Bulb = JsonConvert.DeserializeObject<YeelightBulbModel>(content);
                Bulb.Color = DecodeColor(Bulb.RGB);
            }
            catch (Exception exception)
            { 
                BadResponseDialog.Show(exception.Message);
            }
        }

        private ColorRGB DecodeColor(int rgb) => new ColorRGB
        {
            Blue = rgb & 0x0000FF,
            Green = (rgb & 0x00FF00) >> 8,
            Red = (rgb & 0xFF0000) >> 16
        };

        protected override async Task OnInitializedAsync()
        {
            await GetBulbStatus();
            await base.OnInitializedAsync();
        }
    }
}