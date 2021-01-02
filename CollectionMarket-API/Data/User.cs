using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    public class User: IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Money { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }

        public virtual IList<SaleOffer> SaleOffers { get; set; }
    }
}
