using System;
using System.Linq;
using System.Threading.Tasks;
using CoreNRF.Adapters.LocationAdapter;
using CoreNRF.Adapters.NFAdapter;
using CoreNRF.Adapters.NFServAdapter;
using CoreNRF.Adapters.ServiceAdapter;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;
using CoreNRF.Services.LocationService;
using CoreNRF.Services.NFService;
using CoreNRF.Services.NFServService;
using CoreNRF.Services.ServicesService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;


namespace CoreNRF.Api
{
    [Route("api/[controller]/[action]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RegisterController : ControllerBase
    {
        private readonly INFService _nFService;
        private readonly IServicesService _servicesServices;
        private readonly ILocationService _locationService;
        private readonly ILocationAdapter _locationAdapter;
        private readonly IServiceAdapter _serviceAdapter;
        private readonly INFAdapter _nFAdapter;
        private readonly INFServService _INFServService;
        private readonly INFServAdapter _INFServAdapter;

        public RegisterController(INFService nFService, IServicesService servicesService, ILocationAdapter locationAdapter,
            ILocationService locationService, IServiceAdapter serviceAdapter, INFAdapter nFAdapter, INFServService nFServService,
            INFServAdapter nFServAdapter)
        {
            _nFService = nFService;
            _servicesServices = servicesService;
            _locationAdapter = locationAdapter;
            _locationService = locationService;
            _serviceAdapter = serviceAdapter;
            _nFAdapter = nFAdapter;
            _INFServService = nFServService;
            _INFServAdapter = nFServAdapter;
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
        [HttpPost]
        public async Task<IActionResult> SuscribeNFtoContent([FromBody] NFServiceRegisDto nfRegistServ)
        {
            if (nfRegistServ.NFId == string.Empty)
                return BadRequest("No NF Id in the request");

            var nFServ = _INFServService.GetNFServicesRegisteredByNF(Guid.Parse(nfRegistServ.NFId));
            if (nFServ != null && nFServ.Count() > 0)
                await _INFServService.DeleteRange(nFServ.Select(x => x.Id).AsEnumerable());
            try
            {
                await _INFServService.AddOrUpdateRange(_INFServAdapter.ConvertDtoToNFServs(nfRegistServ));
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

    }
}
