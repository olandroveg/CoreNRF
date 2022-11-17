using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CoreNRF.Models;

namespace CoreNRF.Services.NFService
{
    public interface INFService
    {
        IEnumerable<NF> GetAllNF();
        //NF GetNFbyId(Guid Id);
        Task<Guid> AddOrUpdate(NF nF);
        Task DeleteRange(IEnumerable<Guid> ids);
        NF GetNFById(Guid Id);
        public NF GetNFbyName(string name);
        public Guid GetNfIDbyName(string name);
    }
}
