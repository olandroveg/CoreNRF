using System;
using System.Collections.Generic;

namespace CoreNRF.Models
{
    public class Services
    {
        public Guid Id { get; set; }
        public Guid NfId { get; set; }
        public NF Nf { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string NfBaseAddress { get; set; }
        public string ServiceAPI { get; set; }
        public ICollection<NFServices> NFService { get; set; }
    }
}
