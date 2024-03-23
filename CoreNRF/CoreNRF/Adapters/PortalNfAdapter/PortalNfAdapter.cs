using System;
using CoreNRF.Models;
using CoreNRF.Services.NFService;
using CoreNRF.Services.PortalNFService;

namespace CoreNRF.Adapters.PortalNfAdapter
{
    public class PortalNfAdapter : IPortalNfAdapter
    {
        private readonly INFService _nFService;
        public PortalNfAdapter(INFService nFService)
        {
            _nFService = nFService;
        }

        public PortalNF ConformPortalNFByFields(Guid Id, Guid sourceNFId, Guid targetNFId, string name, DateTime dateTime)
        {
            return new PortalNF
            {
                Id = Id,
                RelationName = name,
                PortalId = sourceNFId,
                NFId = targetNFId == Guid.Empty ? _nFService.GetNfIDbyName(name) : targetNFId,
                DateTime = dateTime
            };
        }

    }
}

