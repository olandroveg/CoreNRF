using System;
using System.Collections.Generic;
using System.Linq;
using CoreNRF.Data;
using CoreNRF.Dtos.ServiceDto;
using CoreNRF.Services.ServicesService;

namespace CoreNRF.Adapters.ServiceAdapter
{
    public class ServiceAdapter: IServiceAdapter
    {
        
        public Models.Services ConvertOneServDtoToService (IncomeServiceDto serviceDto, Guid NfId, Guid serviceId)
        {
            return new CoreNRF.Models.Services
            {
                Id = serviceId != Guid.Empty? serviceId: Guid.Empty,
                Description = serviceDto.Description,
                NfId = NfId,
                Name = serviceDto.Name,
                NfBaseAddress = serviceDto.NfBaseAddress,
                ServiceAPI = serviceDto.ServiceAPI
            };
        }
        public IEnumerable<Models.Services> CovertServicesDtoToServices(IEnumerable<IncomeServiceDto> serviceDtos, Guid nFId)
        {
            return serviceDtos.Select(x => new Models.Services
            {
                Id = Guid.Empty,
                NfId = nFId,
                Description = x.Description,
                Name = x.Name,
                ServiceAPI = x.ServiceAPI,
                NfBaseAddress = x.NfBaseAddress,

            });
        }
    }
}
