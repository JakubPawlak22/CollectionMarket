using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.ModelFactories
{
    public interface IOrderModelFactory
    {
        OrderDTO CreateOrderDTO(Order order);
        Order CreateNewOrder(SaleOffer saleOffer, User buyer);
        void AddToOrder(SaleOffer saleOffer, Order order);
        void RemoveFromCart(SaleOffer saleOffer, Order order);
        void AddAddressInformations(Order order);
    }
}
