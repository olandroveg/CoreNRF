using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;

namespace CoreNRF.Services.NFServService
{
    public interface INFServService
    {
        IEnumerable<NFServices> GetAllNFServices();
        NFServices GetNFServicesById(Guid Id);
        IEnumerable<NFServices> GetNFServicesRegisteredByNF(Guid nfId);
        Task<Guid> AddOrUpdate(NFServices nFService);
        Task AddOrUpdateRange(IEnumerable<NFServices> nFServices);
        Task DeleteRange(IEnumerable<Guid> ids);
    }
}
