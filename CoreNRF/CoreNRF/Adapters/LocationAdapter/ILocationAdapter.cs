using System;
using CoreNRF.Dtos.LocationDto;
using CoreNRF.Models;

namespace CoreNRF.Adapters.LocationAdapter
{
    public interface ILocationAdapter
    {
        Location ConvertLocationDtoToLocation(IncomeLocationDto locationDto, Guid locationId);
        Location ConvertLocationDtoToUpdateLocation(IncomeLocationDto locationDto, Location location);
    }
}
