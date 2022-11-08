using CoreNRF.Data;
using CoreNRF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalService
{
    public class PortalService : IPortalService
    {
        private readonly ApplicationDbContext _context;

        public PortalService (ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }
        public IEnumerable<Portal> GetAllPortals()
        {
            try
            {
                return _context.Portals;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public  Portal GetPortalById(Guid portalId)
        {
            try
            {
                if (portalId == Guid.Empty)
                    throw new ArgumentNullException(nameof(portalId));
                return _context.Portals.Include(e => e.Location).FirstOrDefault(x=> x.Id == portalId);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
            
        }
        public Portal GetPortalByName (string portalName)
        {
            try
            {
                return _context.Portals.Where(e => e.PortalName == portalName).FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
        public async Task<Guid?> AddOrUpdate (Portal portal)
        {
            try
            {
                if (portal.Id == Guid.Empty)
                    await _context.Portals.AddAsync(portal);
                else
                    _context.Portals.Update(portal);
                await _context.SaveChangesAsync();
                return portal.Id;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}
