using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class SaleOfferModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string SellerUsername { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
    }
    public class SaleOfferCreateModel
    {
        public int ProductTypeId { get; set; }
        public int Count { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
    }
    public class SaleOfferUpdateModel
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
    }
}
