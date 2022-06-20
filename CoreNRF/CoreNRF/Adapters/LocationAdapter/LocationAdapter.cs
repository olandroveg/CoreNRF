using System;
using CoreNRF.Dtos.LocationDto;
using CoreNRF.Models;

namespace CoreNRF.Adapters.LocationAdapter
{
    public class LocationAdapter : ILocationAdapter
    {
        public Location ConvertLocationDtoToLocation (IncomeLocationDto locationDto, Guid locationId)
        {
            return new Location
            {
                Id = locationId != Guid.Empty ? locationId: Guid.Empty,
                Latitude = locationDto.Latitude,
                Longitude = locationDto.Longitude,
                Name = locationDto.Name
            };
        }

        public Location ConvertLocationDtoToUpdateLocation(IncomeLocationDto locationDto, Location location)
        {
            location.Latitude = locationDto.Latitude;
            location.Longitude = locationDto.Longitude;
            location.Name = locationDto.Name;
            return location;
        }
    }
}
