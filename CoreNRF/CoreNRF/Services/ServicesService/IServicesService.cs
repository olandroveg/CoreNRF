using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CoreNRF.Services.ServicesService
{
    public interface IServicesService
    {
        IEnumerable<Models.Services> GetAllServices();
        public Models.Services GetServiceById(Guid Id);
        Task<Guid> AddOrUpdateSigleService(Models.Services service);
        Task AddOrUpdateServiceRange(IEnumerable<Models.Services> services);
        Task DeleteRange(IEnumerable<Guid> ids);
    }
}
