using System;
using CoreNRF.Models;
using CoreNRF.Services.PortalNFService;

namespace CoreNRF.Adapters.PortalNfAdapter
{
    public class PortalNfAdapter : IPortalNfAdapter
    {
        
        public PortalNF ConformPortalNFByFields(Guid Id, Guid sourceNFId, Guid targetNFId, string name, DateTime dateTime)
        {
            return new PortalNF
            {
                Id = Id,
                RelationName = name,
                PortalId = sourceNFId,
                NFId = targetNFId,
                DateTime = dateTime
            };
        }

    }
}

