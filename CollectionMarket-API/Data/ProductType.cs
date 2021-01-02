using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    [Table("ProductTypes")]
    public partial class ProductType
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public int? OrderId { get; set; }
        public string Name { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public virtual IList<SaleOffer> SaleOffers { get; set; }
        public virtual IList<AttributeValue> AttributeValues { get; set; }
    }
}
