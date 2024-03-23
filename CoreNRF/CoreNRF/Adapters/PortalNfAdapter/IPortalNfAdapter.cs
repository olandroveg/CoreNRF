using System;
using CoreNRF.Models;

namespace CoreNRF.Adapters.PortalNfAdapter
{
    public interface IPortalNfAdapter
    {
        public PortalNF ConformPortalNFByFields(Guid Id, Guid sourceNFId, Guid targetNFId, string name, DateTime dateTime);
    }
}

