using CoreNRF.Data;
using CoreNRF.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace CoreNRF.Services.PortalNFService
{
    public class PortalNFService : IPortalNFService
    {
        private readonly ApplicationDbContext _context;
        public PortalNFService (ApplicationDbContext context)
        {
            _context = context;
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
        public PortalNF GetPortalNFByPortalIdAndName (Guid portalId, string name)
        {
            try
            {
                if (portalId == Guid.Empty)
                    throw new ArgumentNullException(nameof(portalId));
                return _context.PortalNF.Where(e => e.PortalId == portalId && e.RelationName == name).FirstOrDefault();
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
