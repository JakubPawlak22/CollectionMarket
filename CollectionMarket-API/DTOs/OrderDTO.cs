using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public decimal ProductsPrice { get; set; }
        public decimal ShippingPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string SellerUsername { get; set; }
        public string BuyerUsername { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }
        public string Address { get; set; }
        public OrderState OrderState { get; set; }
        public virtual IList<SaleOfferDTO> SaleOffers { get; set; }
        public EvaluationDTO Evaluation { get; set; }
    }
}
