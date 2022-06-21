using System;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;

namespace CoreNRF.Adapters.NFAdapter
{
    public interface INFAdapter
    {
        NF ConvertNFDtoToNF(IncomeNFDto nfDto, Location location);
        NF ConvertNFDtoToUpdateNF(IncomeNFDto nfDto, NF nF, Location location);
    }
}
