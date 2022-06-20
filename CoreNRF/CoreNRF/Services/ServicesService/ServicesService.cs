using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;
using System.Linq;

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
    }
}
