using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;

namespace CoreNRF.Services.NFService
{
    public class NFService: INFService
    {
        private readonly ApplicationDbContext _context;

        public NFService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<NF> GetAllNF()
        {
            return _context.NFs;
        }
        public NF GetNFbyId(Guid Id)
        {
            return _context.NFs.Find(Id);
        }
        public async Task<Guid> AddOrUpdate (NF nF)
        {
            if (nF.Id == Guid.Empty)
                await _context.NFs.AddAsync(nF);
            else
                _context.NFs.Update(nF);
            await _context.SaveChangesAsync();
            return nF.Id;
        }
        public async Task DeleteRange(IEnumerable<Guid> ids)
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
