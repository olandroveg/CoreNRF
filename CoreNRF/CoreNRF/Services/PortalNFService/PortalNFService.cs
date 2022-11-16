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
        public PortalNFService (ApplicationDbContext context, IServicesService services)
        {
            _context = context;
            _services = services;
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
        public IEnumerable< ServicesAnswerDto> GetPortalNFbyIdsAndName (Guid sourceNFId, Guid targetNFId, string name, bool isPortal)
        {
            try
            {
                var servic = new List<ServicesAnswerDto>();
                var portalNf = new PortalNF();
                if (targetNFId != Guid.Empty)
                    servic = _services.GetAllApiFromNF(Guid.Empty, name).ToList();
                else
                {
                    portalNf = _context.PortalNF.Where(x => x.PortalId == sourceNFId && x.NFId == targetNFId).FirstOrDefault();
                    
                    if (portalNf != null && ((DateTime.Now - portalNf.DateTime).TotalDays > int.Parse(_timeDiscovery)) && !isPortal)
                        servic = _services.GetAllApiFromNF(Guid.Empty, name).ToList();
                    
                    else
                        servic = _services.GetAllApiFromNF(portalNf.Id, "").ToList();
                }
                
               

                //aqui se pregunta por el periodo en que el registro de la NF sera valido antes de caducar y volver a realizar el discovery otra vez.
                //Ademas se pregunta si el portalNf no es de tipo Portal, porque si es de tipo portal no vale tener tiempo de caducidad, pues siempre se asociara
                //a la misma instancea de la NF que esta relacionada.
                if (portalNf != null && ((DateTime.Now - portalNf.DateTime).TotalDays < int.Parse(_timeDiscovery)) && !isPortal)
                    servic = _services.GetAllApiFromNF(portalNf.NFId, "").ToList();
                else if (portalNf != null)
                    servic


                if (sourceNFId == Guid.Empty)
                    throw new ArgumentNullException(nameof(sourceNFId));
                if (targetNFId == Guid.Empty)
                return _context.PortalNF.Where(e => e.PortalId == sourceNFId && e.RelationName == name).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
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
    }
}
