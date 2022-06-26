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
            if (nFServices.Any(x => x.NFId == Guid.Empty))
                throw new Exception("No NF Id");
            try
            {
                foreach (var item in nFServices)
                {
                    await _context.NFServices.AddAsync(item);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                var a = e.Message;
            }
            
        }
        public async Task DeleteRange (IEnumerable<Guid> ids)
        {
            if (ids != null && ids.Any())
            {
                
                foreach (var item in ids)
                {
                    var found = _context.NFServices.Find(item);
                    _context.NFServices.Remove(found);
                }
                await _context.SaveChangesAsync();

            }
        }
    }
}
