﻿using System;
using System.Collections.Generic;

namespace CoreNRF.Dtos.ServiceDto
{
    public class ServicesAnswerDto
    {
        public string ServicesAPI { get; set; }
        public string TargetNFAdd { get; set; }
        public string ServiceName { get; set; }
        public string Description { get; set; }
        public Guid NFId { get; set; }

    }
    
}
