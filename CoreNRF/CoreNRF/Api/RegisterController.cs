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
using CoreNRF.Dtos.PortalDto;
using CoreNRF.Services.PortalService;
using CoreNRF.Services.PortalNFService;

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
        private readonly IPortalService _portalService;
        private readonly IPortalNFService _portalNFService;

        public RegisterController(INFService nFService, IServicesService servicesService, ILocationAdapter locationAdapter,
            ILocationService locationService, IServiceAdapter serviceAdapter, INFAdapter nFAdapter, INFServService nFServService,
            INFServAdapter nFServAdapter, IPortalService portalService, IPortalNFService portalNFService)
        {
            _nFService = nFService;
            _servicesServices = servicesService;
            _locationAdapter = locationAdapter;
            _locationService = locationService;
            _serviceAdapter = serviceAdapter;
            _nFAdapter = nFAdapter;
            _INFServService = nFServService;
            _INFServAdapter = nFServAdapter;
            _portalService = portalService;
            _portalNFService = portalNFService;
        }
        

        [HttpPost]
        public async Task<IActionResult> RegisterOrUpdate([FromBody] IncomeNFDto nFDto)
        {
            try
            {
                var location = new Location();
                var nfId = Guid.Empty;
                if (nFDto.Id != Guid.Empty.ToString())
                {
                    var nF = _nFService.GetNFById(Guid.Parse(nFDto.Id));
                    if (nF != null && nF.Id != Guid.Empty)
                    {
                        location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToUpdateLocation(nFDto.Location, nF.Location));
                        nfId = await _nFService.AddOrUpdate(_nFAdapter.ConvertNFDtoToUpdateNF(nFDto, nF, location));
                        var serviceIds = _servicesServices.GetServiceIdsByNF(nfId);
                        await _servicesServices.DeleteRange(serviceIds);
                        await _servicesServices.AddOrUpdateServiceRange(_serviceAdapter.CovertServicesDtoToServices(nFDto.Services, nfId));

                    }
                    else
                        throw new Exception("No NF with the Id provided");
                }
                else
                {
                    location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToLocation(nFDto.Location, Guid.Empty));
                    nfId = await _nFService.AddOrUpdate(_nFAdapter.ConvertNFDtoToNF(nFDto, location));
                    await _servicesServices.AddOrUpdateServiceRange(_serviceAdapter.CovertServicesDtoToServices(nFDto.Services, nfId));
                }
                return Ok(nfId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }



        [HttpPost]
        public async Task<IActionResult> RegisterPortalOrUpdate([FromBody] PortalRegisterDto portal)
        {
            try
            {
                var location = new Location();
                var portalId = Guid.Empty;
                if (portal.Id != Guid.Empty)
                {
                    var port = _portalService.GetPortalById(portal.Id);
                    if (port != null && port.Id != Guid.Empty)
                    {

                        port.Location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToUpdateLocation(portal.Location, port.Location));
                        port.PortalName = portal.PortalName;
                        portalId = await _portalService.AddOrUpdate(port) ?? Guid.Empty;
                    }
                        
                    else
                        throw new Exception("No portal with the Id provided");
                }
                else
                {
                    //location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToLocation(portal.Location, Guid.Empty));
                    //portalId = await _portalService.AddOrUpdate(new Portal
                    //{
                    //    Id = Guid.Empty,
                    //    Location = new Location
                    //    {
                    //        Id = Guid.Empty,
                    //        Latitude = portal.Location.Latitude,
                    //        Longitude = portal.Location.Longitude,
                    //        Name = portal.Location.Name
                    //    },
                    //    PortalName = portal.PortalName

                    //}) ?? Guid.Empty;
                    portalId = await _portalService.AddOrUpdate(new Portal
                    {
                        Id = Guid.Empty,
                        Location = await _locationService.AddOrUpdate(_locationAdapter.ConvertLocationDtoToLocation(portal.Location, Guid.Empty)),
                        PortalName = portal.PortalName

                    }) ?? Guid.Empty;


                }
                return Ok(portalId);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
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
