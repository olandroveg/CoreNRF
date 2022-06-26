using System.Collections.Generic;

namespace CoreNRF.Dtos.Notification
{
    public class NotifSuscripDto
    {
        public string NRFid { get; set; }
        public string NFsuscriberId { get; set; }
        public ICollection <string> ServiceSucribed { get; set; }
    }
}
