using CoreNRF.Dtos.ServiceDto;
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
        IEnumerable<Guid> GetServiceIdsByNF(Guid nFId);
        IEnumerable<ServicesAnswerDto> GetServAPItoNF(IEnumerable<string> serviceRqts);
        Task<IEnumerable<ServicesAnswerDto>> GetServAPIAfterDisclaim(IEnumerable<ServicesAnswerDto> serviceAnswerDisclaim, IEnumerable<string> serviceRqts);
        Task DisableNFfromCatalog(IEnumerable<ServicesAnswerDto> serviceAnswerDisclaim);
    }
}
