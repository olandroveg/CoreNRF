using System;
using System.Collections.Generic;
using CoreNRF.Dtos.ServiceDto;

namespace CoreNRF.Adapters.ServiceAdapter
{
    public interface IServiceAdapter
    {

        Models.Services ConvertOneServDtoToService(IncomeServiceDto serviceDto, Guid NfId, Guid serviceId);
        IEnumerable<Models.Services> CovertServicesDtoToServices(IEnumerable<IncomeServiceDto> serviceDtos, Guid nFId);
    }
}
