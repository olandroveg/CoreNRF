﻿using CoreNRF.Adapters.LocationAdapter;
using CoreNRF.Adapters.NFAdapter;
using CoreNRF.Adapters.ServiceAdapter;
using CoreNRF.Dtos.ServiceDto;
using CoreNRF.Services.LocationService;
using CoreNRF.Services.NFService;
using CoreNRF.Services.PortalNFService;
using CoreNRF.Services.PortalService;
using CoreNRF.Services.ServicesService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNRF.Api
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly INFService _nFService;
        private readonly IServicesService _servicesServices;
        private readonly ILocationService _locationService;
        private readonly ILocationAdapter _locationAdapter;
        private readonly IServiceAdapter _serviceAdapter;
        private readonly INFAdapter _nFAdapter;
        private readonly IPortalNFService _portalNFService;
        private readonly IPortalService _portalService;
        public RouteController(INFService nFService, IServicesService servicesService, ILocationAdapter locationAdapter,
            ILocationService locationService, IServiceAdapter serviceAdapter, INFAdapter nFAdapter, IPortalNFService portalNFService,
            IPortalService portalService)
        {
            _nFService = nFService;
            _servicesServices = servicesService;
            _locationAdapter = locationAdapter;
            _locationService = locationService;
            _serviceAdapter = serviceAdapter;
            _nFAdapter = nFAdapter;
            _portalNFService = portalNFService;
            _portalService = portalService;
        }
        [HttpPost]
        public async Task< IActionResult> ServiceRequest([FromBody] ServiceReqstDto serviceRqst)
        {
            if (serviceRqst.NFId == null || serviceRqst.ServiceNames.Any(x=> x == string.Empty))
                return BadRequest();
            IEnumerable<ServicesAnswerDto> serv = null;
            if (!serviceRqst.DisclaimNotWork)
            {
                serv = _servicesServices.GetServAPItoNF(serviceRqst.ServiceNames.AsEnumerable());
            }
            else
            {
                serv = await _servicesServices.GetServAPIAfterDisclaim(serviceRqst.serviceNotWork, serviceRqst.ServiceNames.AsEnumerable());
            }
           
            return Ok(serv);
        }
        [HttpPost]
        public async Task<IActionResult> AllApisNF(Guid portalId, Guid nfId, string targetNFName)
        {
            try
            {
                var portal = _portalService.GetPortalById(portalId);
                if (portal == null)
                    throw new Exception("portalId not in RNF DB");
            var apis = _servicesServices.GetAllApiFromNF(nfId, targetNFName);
                return apis != null ? Ok(apis) : BadRequest("NfId or Name not found");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
