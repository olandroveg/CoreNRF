using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;
using System.Linq;
using CoreNRF.Dtos.ServiceDto;
using Microsoft.EntityFrameworkCore;

namespace CoreNRF.Services.ServicesService
{
    public class ServicesService : IServicesService
    {
        private readonly ApplicationDbContext _context;

        public ServicesService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Models.Services> GetAllServices()
        {
            return (IEnumerable<Models.Services>)_context.Services;
        }
        public Models.Services GetServiceById(Guid Id)
        {
            return _context.Services.Find(Id);
        }
        public async Task<Guid> AddOrUpdateSigleService(Models.Services service)
        {
            if (service.Id == Guid.Empty)
                await _context.Services.AddAsync(service);
            else
                _context.Services.Update(service);
            await _context.SaveChangesAsync();
            return service.Id;
        }
        public async Task AddOrUpdateServiceRange (IEnumerable<Models.Services> services)
        {
            if (services.Any(x => x.Id == Guid.Empty))
                await _context.Services.AddRangeAsync(services);
            else
                _context.Services.UpdateRange(services);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRange(IEnumerable<Guid> ids)
        {
            if (ids != null && ids.Any())
            {

                foreach (var item in ids)
                {
                    var found = _context.Services.Find(item);
                    _context.Services.Remove(found);
                }
                await _context.SaveChangesAsync();

            }
        }
        public IEnumerable<Guid> GetServiceIdsByNF(Guid nFId)
        {
            return _context.Services.Where(x => x.NfId == nFId).Select(x => x.Id);
        }
        public IEnumerable<ServicesAnswerDto> GetServAPItoNF (IEnumerable<string> serviceRqts)
        {
            return serviceRqts.Select(x =>
           {
               return _context.Services.Include(e=> e.Nf).Where(e => e.Name == x && e.Nf.State == "available").Select(g => new ServicesAnswerDto
               {
                   ServicesAPI = g.ServiceAPI,
                   TargetNFAdd = g.NfBaseAddress,
                   ServiceName = g.Name
               }).FirstOrDefault();
           });
            
        }
        public async Task< IEnumerable<ServicesAnswerDto>> GetServAPIAfterDisclaim(IEnumerable<ServicesAnswerDto> serviceAnswerDisclaim, IEnumerable<string> serviceRqts)
        {
            var serviceAnswer = new List<ServicesAnswerDto>();
            foreach(var item in serviceAnswerDisclaim)
            {
                // HASTA AHORA NO HAY PARAMETROS DE EVAUACION DE CUAL NF ENTRE OTRAS DE SIMILAR TIPO (IDENTICOS SERVICIOS) SERA LA QUE SE DEVOLVERA POR EL NRF.
                // SE PRESUME QUE HABRA QUE HACER ANALISIS DE DISTANCIA, NIVEL DE OCUPACION, ETC. POR LO PRONTO SE TOMARA EL 1ER NF DE ESE TIPO QUE ESTA REGISTRADO EN BD (FirstOrDefault())
                var serv = serviceRqts.Select(x =>
                {
                    return _context.Services.Include(e=>e.Nf).Where(e => e.Nf.State== "available" && e.Name == x && e.NfBaseAddress != item.TargetNFAdd).Select(g => new ServicesAnswerDto
                    {
                        ServicesAPI = g.ServiceAPI,
                        TargetNFAdd = g.NfBaseAddress,
                        ServiceName = g.Name
                    }).FirstOrDefault();
                });
                if (serv != null)
                    serviceAnswer.AddRange(serv);

            }
            await DisableNFfromCatalog(serviceAnswerDisclaim);
            return serviceAnswer;
        }
        public async Task DisableNFfromCatalog (IEnumerable<ServicesAnswerDto> serviceAnswerDisclaim)
        {
            foreach (var item in serviceAnswerDisclaim)
            {
                var nFtoDisable = _context.Services.Include(x => x.Nf).Where(e => e.NfBaseAddress == item.TargetNFAdd).Select(x => x.Nf).FirstOrDefault();
                nFtoDisable.State = "disable";
                _context.NFs.Update(nFtoDisable);
            }
            await _context.SaveChangesAsync();
        }
        public IEnumerable <Models.Services> GetServicesByNames (IEnumerable<string> names)
        {
            return names.Select(x =>
            {
                return _context.Services.Where(a => a.Name == x).FirstOrDefault();
            });
        }
        public Models.Services GetServicesByName(string name)
        {
            
                return _context.Services.Where(a => a.Name == name).FirstOrDefault();
        }

    }
}
