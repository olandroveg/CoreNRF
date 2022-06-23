using System.Collections.Generic;

namespace CoreNRF.Dtos.ServiceDto
{
    public class ServiceReqstDto
    {
        public string NFId { get; set; }       
        public string NF { get; set; }
        public ICollection <string> ServiceNames { get; set; }
        public float BusyIndex { get; set; }
        public bool DisclaimNotWork { get; set; }
        public IEnumerable<ServicesAnswerDto> serviceNotWork { get; set; }
    }
}
