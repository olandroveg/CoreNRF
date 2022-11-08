using CoreNRF.Models;
using System;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalNFService
{
    public interface IPortalNFService
    {
        public PortalNF GetPortalNFById(Guid Id);
        public PortalNF GetPortalNFByPortalIdAndName(Guid portalId, string name);
        public Task<Guid?> AddOrUpdate(PortalNF portalNF);
    }
}
