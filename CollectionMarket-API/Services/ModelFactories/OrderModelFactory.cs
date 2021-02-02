using AutoMapper;
using CollectionMarket_API.Contracts.ModelFactories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.ModelFactories
{
    public class OrderModelFactory : IOrderModelFactory
    {
        private readonly IMapper _mapper;
        public OrderModelFactory(IMapper mapper)
        {
            _mapper = mapper;
        }
        private const decimal SHIPPING_PRICE = 2;
        public OrderDTO CreateOrderDTO(Order order)
        {
            var dto = new OrderDTO
            {
                BuyerUsername = order.Buyer.UserName,
                SellerUsername = order.SaleOffers.FirstOrDefault().Seller.UserName,
                SaleOffers = _mapper.Map<IList<SaleOfferDTO>>(order.SaleOffers),
                ShippingPrice = SHIPPING_PRICE,
                ProductsPrice = CalculateProductsPrice(order.SaleOffers),
                TotalPrice = order.Price,
                Id = order.Id,
                OrderState = (OrderState)order.OrderState,
                Address = order.Address,
                City = order.City,
                Postcode = order.PostCode,
                Evaluation = CreateEvaluationModel(order)
            };
            return dto;
        }

        private EvaluationDTO CreateEvaluationModel(Order order)
        {
            if (order.Evaluation != null)
                return new EvaluationDTO
                {
                    Evaluation = order.Evaluation.Value,
                    EvaluationDescription = order.EvaluationDescription
                };
            else return null;
        }

        private decimal CalculateProductsPrice(IList<SaleOffer> saleOffers)
        {
            decimal totalPrice = 0;
            foreach (var offer in saleOffers)
            {
                totalPrice += offer.PricePerItem * offer.Count;
            }
            return totalPrice;
        }

        public Order CreateNewOrder(SaleOffer saleOffer, User buyer)
        {
            Order order = new Order
            {
                Buyer = buyer,
                OrderState = (int)OrderState.InCart,
                SaleOffers = new List<SaleOffer>()
            };
            order.SaleOffers.Add(saleOffer);
            order.Price = CalculateProductsPrice(order.SaleOffers) + SHIPPING_PRICE;
            return order;
        }

        public void AddToOrder(SaleOffer saleOffer, Order order)
        {
            order.SaleOffers.Add(saleOffer);
            order.Price = CalculateProductsPrice(order.SaleOffers) + SHIPPING_PRICE;
        }

        public void RemoveFromCart(SaleOffer saleOffer, Order order)
        {
            order.SaleOffers.Remove(saleOffer);
            order.Price = CalculateProductsPrice(order.SaleOffers) + SHIPPING_PRICE;
        }

        public void AddAddressInformations(Order order)
        {
            order.Address = order.Buyer.Address;
            order.City = order.Buyer.City;
            order.PostCode = order.Buyer.PostCode;
        }
    }
}
