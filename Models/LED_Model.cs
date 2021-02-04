using System.Collections.Generic;

namespace RaspberryPiHomeControlApiClient.Models
{
    public class LED_Model
    {
        public List<int> Pins { get; set; }

        public bool IsActive { get; set; }

        public int Number { get; set; }

        public bool IsRGB { get; set; }

        public List<float> Color { get; set; }
    }
}