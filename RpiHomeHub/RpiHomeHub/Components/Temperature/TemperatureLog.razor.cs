﻿using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace RpiHomeHub.Components.Temperature
{
    public partial class TemperatureLog : ComponentBase
    {
        [Inject]
        private HttpClient httpClient { get; set; }

        private List<string> LogLines { get; set; }

        private async Task FetchLog()
        {
            var response = await httpClient.GetAsync("temp_log");
            var content = await response.Content.ReadAsStringAsync();
            LogLines = JsonConvert.DeserializeObject<List<string>>(content);
            LogLines.Reverse();
        }

        private async Task ClearLog()
        {
            var response = await httpClient.DeleteAsync("temp_log");
            if (response.StatusCode == HttpStatusCode.OK)
            {
                LogLines = new List<string>();
            }
            //else ErrorDialog?
        }

        private int GetLinesCount() => LogLines is not null ? LogLines.Count : 0;

        protected override async Task OnInitializedAsync()
        {
            await FetchLog();
            await base.OnInitializedAsync();
        }
    }
}
