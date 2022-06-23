using System;
using System.Collections.Generic;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;

namespace CoreNRF.Adapters.NFServAdapter
{
    public interface INFServAdapter
    {
        IEnumerable<NFServices> ConvertDtoToNFServs(NFServiceRegisDto nFServiceDto);
    }
}
