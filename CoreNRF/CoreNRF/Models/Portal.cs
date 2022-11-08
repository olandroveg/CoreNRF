using System;
using System.Collections.Generic;

namespace CoreNRF.Models
{
    public class Portal
    {
        public Guid Id { get; set; }
        public string PortalName { get; set; }
        public Location Location { get; set; }
        public ICollection <PortalNF> PortalNFs { get; set; }
    }
}
