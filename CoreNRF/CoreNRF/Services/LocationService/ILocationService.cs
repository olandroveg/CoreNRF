using System;
using System.Threading.Tasks;
using CoreNRF.Models;

namespace CoreNRF.Services.LocationService
{
    public interface ILocationService
    {
        Location GetLocationById(Guid Id);
        Task<Location> AddOrUpdate(Location location);
    }
}
