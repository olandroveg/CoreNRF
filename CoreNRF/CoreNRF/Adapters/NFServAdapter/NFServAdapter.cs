using System;
using System.Collections.Generic;
using System.Linq;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;
using CoreNRF.Services.NFService;
using CoreNRF.Services.NFServService;
using CoreNRF.Services.ServicesService;

namespace CoreNRF.Adapters.NFServAdapter
{
    public class NFServAdapter: INFServAdapter
    {
        private readonly IServicesService _ServicesService;

        public NFServAdapter(IServicesService servicesService)
        {
            _ServicesService =servicesService;
        }
       public IEnumerable<NFServices> ConvertDtoToNFServs(NFServiceRegisDto nFServiceDto)
        {
            return nFServiceDto.ServiceName.Select(x => new NFServices
            {
                NFId= nFServiceDto.NFId != string.Empty ? Guid.Parse(nFServiceDto.NFId) : Guid.Empty,
                ServiceId = _ServicesService.GetServicesByName(x).Id
            });
            
        }
    }
}
