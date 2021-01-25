using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class SaleOfferDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string SellerUsername { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
    }
    public class SaleOfferUpdateDTO
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
    }
    public class SaleOfferCreateDTO
    {
        public int ProductTypeId { get; set; }
        public int Count { get; set; }
        public decimal PricePerItem { get; set; }
        public Condition Condition { get; set; }
        public string Description { get; set; }
    }
}
