using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class OrderDTO
    {
        public virtual IList<SaleOfferDTO> SaleOffers { get; set; }
    }
}
