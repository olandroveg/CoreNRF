﻿using System;
using System.Collections;

namespace CoreNRF.Models
{
    public class NF
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }
        public ICollection Services { get; set; }
        public float BusyIndex { get; set; }
        public string State { get; set; }
    }
}