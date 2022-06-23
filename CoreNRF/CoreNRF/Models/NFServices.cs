using System;
using System.Collections.Generic;

namespace CoreNRF.Models
{
    public class NFServices
    {
        public Guid Id { get; set; }
        public Guid NFId { get; set; }
        public NF NF { get; set; }
        public Guid ServiceId { get; set; }
        public Services Service { get; set; }
    }
}
