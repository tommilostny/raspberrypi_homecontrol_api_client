using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using RaspberryPiHomeControlApiClient.Models;

namespace RaspberryPiHomeControlApiClient.Pages
{
    public partial class LEDs_ControlPage
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private List<LED_Model> Leds { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var response = await httpClient.GetAsync("led");
            var content = await response.Content.ReadAsStringAsync();
            Leds = JsonConvert.DeserializeObject<List<LED_Model>>(content);
            
            await base.OnInitializedAsync();
        }
    }
}