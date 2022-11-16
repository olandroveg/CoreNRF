using System;

namespace CoreNRF.Dtos.NRFDto
{
    public class PortalOrNFDiscoverDto
    {
       public Guid SourceNFId { get; set; }
       public Guid PortalId { get; set; }
       public Guid TargetNFId { get; set; }
       public string NFName { get; set; }
       public bool isPortal { get; set; }
    }
}
