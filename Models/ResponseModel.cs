using System.Net;
using Newtonsoft.Json;

namespace RaspberryPiHomeControlApiClient.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }

        [JsonIgnore]
        public HttpStatusCode Code { get; set; }
    }
}