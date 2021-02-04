using Microsoft.AspNetCore.Components;
using RaspberryPiHomeControlApiClient.Models;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;

namespace RaspberryPiHomeControlApiClient.Components
{
    public partial class ErrorDialog : ComponentBase
    {
        private bool Visible { get; set; }

        private ResponseModel Content { get; set; } = new ResponseModel { Message="Hey", Code=HttpStatusCode.Continue };

        private void CloseDialog() => Visible = false;

        public async Task Show(HttpResponseMessage response)
        {
            var content = await response.Content.ReadAsStringAsync();
            Content = JsonConvert.DeserializeObject<ResponseModel>(content);
            Content.Code = response.StatusCode;
            Visible = true;
        }

        public void Show(string message , HttpStatusCode code = HttpStatusCode.ExpectationFailed)
        {
            Content = new ResponseModel { Message = message, Code = code };
            Visible = true;
        }
    }
}