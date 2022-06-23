using System.Collections.Generic;

namespace CoreNRF.Dtos.NRFDto
{
    public class NFServiceRegisDto
    {
        public string Id { get; set; }
        public ICollection< string> ServiceName { get; set; }
    }
}
