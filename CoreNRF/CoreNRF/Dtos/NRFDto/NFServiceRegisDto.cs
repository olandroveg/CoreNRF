using System.Collections.Generic;

namespace CoreNRF.Dtos.NRFDto
{
    public class NFServiceRegisDto
    {
        public string NFId { get; set; }
        public ICollection< string> ServiceName { get; set; }
    }
}
