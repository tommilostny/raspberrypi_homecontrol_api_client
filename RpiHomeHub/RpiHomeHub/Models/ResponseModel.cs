using System.Net;
using Newtonsoft.Json;

namespace RpiHomeHub.Models
{
    public class ResponseModel
    {
        public string Message { get; set; }

        [JsonIgnore]
        public HttpStatusCode Code { get; set; }
    }
}
