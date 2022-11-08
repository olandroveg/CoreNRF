using System;

namespace CoreNRF.Models
{
    public class PortalNF
    {
        public Guid Id { get; set; }
        public string RelationName { get; set; }
        public Guid PortalId { get; set; }
        public Portal Portal { get; set; }
        public Guid NFId { get; set; }
        public NF NF { get; set; }
    }
}
