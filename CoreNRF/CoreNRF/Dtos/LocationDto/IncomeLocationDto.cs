using System;
using CoreNRF.Models;

namespace CoreNRF.Dtos.LocationDto
{
    public class IncomeLocationDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public Location ConvertToLocation()
        {
            return new Location
            {
                Id = Guid.Parse( this.Id),
                Name = this.Name,
                Latitude = this.Latitude,
                Longitude = this.Longitude
            };
        }
    }
}
