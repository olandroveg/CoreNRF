using System;
using System.Threading.Tasks;
using CoreNRF.Adapters.LocationAdapter;
using CoreNRF.Adapters.NFAdapter;
using CoreNRF.Adapters.ServiceAdapter;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;
using CoreNRF.Services.LocationService;
using CoreNRF.Services.NFService;
using CoreNRF.Services.ServicesService;
using Microsoft.AspNetCore.Mvc;


namespace CoreNRF.Api
{
    [Route("api/[controller]/[action]")]
    public class RegisterController : ControllerBase
    {
        private readonly INFService _nFService;
        private readonly IServicesService _servicesServices;
        private readonly ILocationService _locationService;
        private readonly ILocationAdapter _locationAdapter;
        private readonly IServiceAdapter _serviceAdapter;
        private readonly INFAdapter _nFAdapter;

        public RegisterController(INFService nFService, IServicesService servicesService, ILocationAdapter locationAdapter,
            ILocationService locationService, IServiceAdapter serviceAdapter, INFAdapter nFAdapter)
        {
            _nFService = nFService;
            _servicesServices = servicesService;
            _locationAdapter = locationAdapter;
            _locationService = locationService;
            _serviceAdapter = serviceAdapter;
            _nFAdapter = nFAdapter;
        }
        [HttpPost]
        public async Task< IActionResult> Register([FromBody] IncomeNFDto nFDto)
        {
                var location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToLocation(nFDto.Location, Guid.Empty));
                var nfId = await _nFService.AddOrUpdate(_nFAdapter.ConvertNFDtoToNF(nFDto, location));
                await _servicesServices.AddOrUpdateServiceRange(_serviceAdapter.CovertServicesDtoToServices(nFDto.Services, nfId));
                return Ok(nfId);     
                    
        }
        [HttpPost]
        public async Task<IActionResult> UpdateRegistration([FromBody] IncomeNFDto nFDto)
        {
            if (nFDto.Id!= string.Empty)
            {
                var nF = _nFService.GetNFById(Guid.Parse(nFDto.Id));
                var location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToUpdateLocation(nFDto.Location, nF.Location));
                var nfId = await _nFService.AddOrUpdate(_nFAdapter.ConvertNFDtoToUpdateNF(nFDto, nF, location));
                var serviceIds = _servicesServices.GetServiceIdsByNF(nfId);
                await _servicesServices.DeleteRange(serviceIds);
                await _servicesServices.AddOrUpdateServiceRange(_serviceAdapter.CovertServicesDtoToServices(nFDto.Services, nfId));
                return Ok(nfId);
            }
            return Ok("NF not found");
        }

    }
}
