using CoreNRF.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalService
{
    public interface IPortalService
    {
        public IEnumerable<Portal> GetAllPortals();
        public Portal GetPortalById(Guid portalId);
        public Portal GetPortalByName(string portalName);
        public Task<Guid?> AddOrUpdate(Portal portal);
    }
}
