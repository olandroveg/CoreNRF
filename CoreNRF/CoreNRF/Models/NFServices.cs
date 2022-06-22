using System;
using System.Collections.Generic;

namespace CoreNRF.Models
{
    public class NFServices
    {
        public Guid Id { get; set; }
        public ICollection<NF> nFs { get; set; }
        public ICollection<Services> Services { get; set; }
    }
}
