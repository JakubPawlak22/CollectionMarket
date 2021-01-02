using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    [Table("SaleOffers")]
    public class SaleOffer
    {
        public int Id { get; set; }
        public int Count { get; set; }
        public string SellerId { get; set; }
        public int ProductTypeId { get; set; }
        public decimal PricePerItem { get; set; }
        public int Condition { get; set; }
        public bool IsInCart { get; set; }
        public string Description { get; set; }

        [ForeignKey("SellerId")]
        public virtual User Seller { get; set; }

        [ForeignKey("ProductTypeId")]
        public virtual ProductType ProductType { get; set; }
    }
}
