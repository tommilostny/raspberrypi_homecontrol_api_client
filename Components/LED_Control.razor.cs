using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Linq;
using Newtonsoft.Json;
using raspberrypi_homecontrol_api_client.Models;

namespace raspberrypi_homecontrol_api_client.Components
{
    public partial class LED_Control
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        [Parameter]
        public LED_Model Led { get; set; }

        private async Task LED_Toggle()
        {
            await httpClient.GetAsync($"led/{Led.Number}/toggle");
            Led.IsActive = !Led.IsActive;
        }
    }
}