using CoreNRF.Adapters.PortalNfAdapter;
using CoreNRF.Data;
using CoreNRF.Dtos.ServiceDto;
using CoreNRF.Models;
using CoreNRF.Services.ServicesService;
using CoreNRF.Utils;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalNFService
{
    public class PortalNFService : IPortalNFService
    {
        private readonly ApplicationDbContext _context;
        private readonly string _timeDiscovery;
        private readonly IServicesService _services;
        private readonly IPortalNfAdapter _portalNfAdapter;
        public PortalNFService (ApplicationDbContext context, IServicesService services, IPortalNfAdapter portalNfAdapter)
        {
            _context = context;
            _services = services;
            _portalNfAdapter = portalNfAdapter;
            _timeDiscovery = StaticConfigurationManager.AppSetting["TimeAliveDiscoveryDays"];
        }
        public PortalNF GetPortalNFById (Guid Id)
        {
            try
            {
                return _context.PortalNF.Find(Id);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task< IEnumerable< ServicesAnswerDto>> GetPortalNFbyIdsAndName (Guid sourceNFId, Guid targetNFId, string name, bool isPortal)
        {
            try
            {
                var servic = new List<ServicesAnswerDto>();
                var portalNf = new PortalNF();
                if (targetNFId != Guid.Empty)
                {
                    portalNf = _context.PortalNF.Where(x => x.PortalId == sourceNFId && x.NFId == targetNFId).FirstOrDefault();
                    //aqui se pregunta por el periodo en que el registro de la NF sera valido antes de caducar y volver a realizar el discovery otra vez.
                    //Ademas se pregunta si el portalNf no es de tipo Portal, porque si es de tipo portal no vale tener tiempo de caducidad, pues siempre se asociara
                    //a la misma instancea de la NF que esta relacionada.
                    if (portalNf != null && ((DateTime.Now - portalNf.DateTime).TotalDays < int.Parse(_timeDiscovery)) && !isPortal)
                        servic = _services.GetAllApiFromNF(portalNf.Id, "").ToList();
                    else
                    {
                        if (portalNf != null && ((DateTime.Now - portalNf.DateTime).TotalDays > int.Parse(_timeDiscovery)) && !isPortal)
                            await DeletePortalNF(portalNf);
                        servic = _services.GetAllApiFromNF(Guid.Empty, name).ToList();
                    }  
                }
                else
                    servic = _services.GetAllApiFromNF(Guid.Empty, name).ToList();

                return servic;
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        
        public async Task<Guid> CheckAndAdd (Guid sourceNFId, Guid targetNFId, string name)
        {
            try
            {
                var portalNF = _context.PortalNF.Where(x => x.PortalId == sourceNFId && x.NFId == targetNFId).FirstOrDefault();
                if (portalNF == null)
                    await AddOrUpdate(_portalNfAdapter.ConformPortalNFByFields(Guid.Empty, sourceNFId, targetNFId, name, DateTime.Now));
                return portalNF.Id;
            }
            catch  (Exception e)
            {
                Console.WriteLine(e.Message);
                return Guid.Empty;
            }
        }
        
        public async Task <Guid?> AddOrUpdate (PortalNF portalNF)
        {
            try
            {
                if (portalNF.Id == Guid.Empty)
                    await _context.AddAsync(portalNF);
                else
                    _context.Update(portalNF);
                await _context.SaveChangesAsync();
                return portalNF.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task DeletePortalNF (PortalNF portalNF)
        {
            try
            {
                _context.PortalNF.Remove(portalNF);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
