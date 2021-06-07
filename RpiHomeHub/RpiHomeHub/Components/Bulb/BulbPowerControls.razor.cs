using Microsoft.AspNetCore.Components;
using RpiHomeHub.Models;
using RpiHomeHub.Services;
using System.Threading.Tasks;

namespace RpiHomeHub.Components.Bulb
{
    public partial class BulbPowerControls : ComponentBase
    {
        [Inject]
        private YeelightBulbService yeelightService { get; set; }

        [Parameter]
        public YeelightBulbModel Bulb { get; set; }

        private async Task Toggle()
        {
            Bulb = await yeelightService.Toggle();
        }

        protected override async Task OnInitializedAsync()
        {
            if (Bulb is null)
            {
                Bulb = await yeelightService.GetStatus();
            }
            await base.OnInitializedAsync();
        }
    }
}
