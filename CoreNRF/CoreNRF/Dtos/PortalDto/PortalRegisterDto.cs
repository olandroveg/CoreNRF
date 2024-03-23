using CoreNRF.Dtos.LocationDto;
using CoreNRF.Models;
using System;

namespace CoreNRF.Dtos.PortalDto
{
    public class PortalRegisterDto
    {
        public Guid Id { get; set; }
        public string PortalName { get; set; }
        public IncomeLocationDto Location { get; set; }
        public float BusyIndex { get; set; }
    }
}
