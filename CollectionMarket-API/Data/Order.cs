using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    [Table("Orders")]
    public class Order
    {
        public int Id { get; set; }
        public string BuyerId { get; set; }
        public string City { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public decimal Price { get; set; }
        public int OrderState { get; set; }
        public int Evaluation { get; set; }
        public string EvaluationDescription { get; set; }

        [ForeignKey("BuyerId")]
        public virtual User Buyer { get; set; }
        public virtual IList<SaleOffer> SaleOffers { get; set; }
    }
}
