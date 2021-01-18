using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Filters
{
    public class SaleOfferFilters
    {
        public int? ProductTypeId { get; set; }
        public string SellerUsername { get; set; }
    }
}
