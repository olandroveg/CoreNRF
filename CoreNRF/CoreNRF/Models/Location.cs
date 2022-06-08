﻿using System;

namespace CoreNRF.Models
{
    public class Location
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
