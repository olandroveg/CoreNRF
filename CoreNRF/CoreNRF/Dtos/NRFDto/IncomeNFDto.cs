using CoreNRF.Dtos.LocationDto;
using CoreNRF.Dtos.ServiceDto;
using System;
using System.Collections.Generic;

namespace CoreNRF.Dtos.NRFDto
{
    public class IncomeNFDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IncomeLocationDto Location { get; set; }
        public ICollection<IncomeServiceDto> Services { get; set; }
        public float BusyIndex { get; set; }
        public string state { get; set; }
    }
}
