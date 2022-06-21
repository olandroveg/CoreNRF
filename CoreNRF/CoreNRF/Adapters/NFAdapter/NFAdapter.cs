using System;
using CoreNRF.Dtos.NRFDto;
using CoreNRF.Models;

namespace CoreNRF.Adapters.NFAdapter
{
    public class NFAdapter: INFAdapter
    {
        public NF ConvertNFDtoToNF (IncomeNFDto nfDto, Location location)
        {
            return new NF
            {
                Id = nfDto.Id != string.Empty ? Guid.Parse(nfDto.Id) : Guid.Empty,
                BusyIndex = nfDto.BusyIndex,
                Name = nfDto.Name,
                State = nfDto.state,
                Location = location

            };
        }
        public NF ConvertNFDtoToUpdateNF(IncomeNFDto nfDto,NF nF, Location location)
        {
            nF.BusyIndex = nfDto.BusyIndex;
            nF.Location = location;
            nF.Name = nfDto.Name;
            nF.State = nfDto.state;
            return nF;
        }
    }
}
