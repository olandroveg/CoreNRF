using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreNRF.Services.NFServService
{
    public class NFServService : INFServService
    {
        private readonly ApplicationDbContext _context;

        public NFServService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<NFServices> GetAllNFServices()
        {
            return _context.NFServices.AsEnumerable();
        }
        public NFServices GetNFServicesById(Guid Id)
        {
            return _context.NFServices.Where(x => x.Id == Id).FirstOrDefault();
        }
        public IEnumerable<NFServices> GetNFServicesRegisteredByNF (Guid nfId)
        {
            return _context.NFServices.Where(x => x.NFId == nfId).AsEnumerable();
        }
        public async Task<Guid> AddOrUpdate (NFServices nFService)
        {
            if (nFService.Id == Guid.Empty)
                await _context.NFServices.AddAsync(nFService);
            else
                _context.NFServices.Update(nFService);
            await _context.SaveChangesAsync();
            return nFService.Id;
        }
        public async Task AddOrUpdateRange (IEnumerable<NFServices> nFServices)
        {
            if (nFServices.Any(x => x.Id == Guid.Empty))
            {
                foreach (var item in nFServices)
                {
                    await _context.NFServices.AddAsync(item);
                }
            }
            else
            {
                foreach (var item in nFServices)
                {
                    _context.NFServices.Update(item);
                }
            }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteRange (IEnumerable<Guid> ids)
        {
            if (ids != null && ids.Any())
            {
                
                foreach (var item in ids)
                {
                    var found = _context.NFs.Find(item);
                    _context.NFs.Remove(found);
                }
                await _context.SaveChangesAsync();

            }
        }
    }
}
