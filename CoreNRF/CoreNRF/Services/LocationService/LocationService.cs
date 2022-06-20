using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreNRF.Data;
using CoreNRF.Models;

namespace CoreNRF.Services.LocationService
{
    public class LocationService: ILocationService
    {
        private readonly ApplicationDbContext _context;
        public LocationService(ApplicationDbContext context)
        {
            _context = context;
        }
        public Location GetLocationById(Guid Id)
        {
            return _context.Locations.Find(Id);
        }
        public async Task<Location> AddOrUpdate(Location location)
        {
            if (location.Id == Guid.Empty)
                await _context.Locations.AddAsync(location);
            else
                _context.Locations.Update(location);
            await _context.SaveChangesAsync();
            return location;
        }
    }
}
