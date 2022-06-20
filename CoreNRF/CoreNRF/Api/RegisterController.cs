using System;
using System.Threading.Tasks;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;
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
        public RegisterController(INFService nFService, IServicesService servicesService)
        {
            _nFService = nFService;
            _servicesServices = servicesService;
        }
        [HttpPost]
        public async Task< IActionResult> Register([FromBody] IncomeNFDto nFDto)
        {
            var nF = new NF
            {
                Id = Guid.Parse(nFDto.Id),
                State = nFDto.state,
                Name = nFDto.Name
            };
            var nfId = await _nFService.AddOrUpdate(nF);
            if (nfId != Guid.Empty)
            {
                foreach (var item in nF.Services)
                {
                    item.NfId = nfId;
                }
                await _servicesServices.AddOrUpdateServiceRange(nF.Services);
            }
            return Ok(nF.Id);     
                    
        }

    }
}
