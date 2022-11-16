using CoreNRF.Dtos.ServiceDto;
using CoreNRF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalNFService
{
    public interface IPortalNFService
    {
        public PortalNF GetPortalNFById(Guid Id);
        public Task<Guid?> AddOrUpdate(PortalNF portalNF);
        public Task<IEnumerable<ServicesAnswerDto>> GetPortalNFbyIdsAndName(Guid sourceNFId, Guid targetNFId, string name, bool isPortal);
        public Task<Guid> CheckAndAdd(Guid sourceNFId, Guid targetNFId, string name);
    }
}
