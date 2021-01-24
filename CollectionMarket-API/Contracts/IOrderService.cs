using CollectionMarket_API.DTOs;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IOrderService
    {
        Task<bool> IsOrderBuyer(int orderId, string loggedUserName);
        Task<bool> IsOrderSeller(int orderId, string loggedUserName);
        Task<OrderDTO> Get(int id);
        Task<IList<OrderDTO>> GetCart(string username);
        Task<IList<OrderDTO>> Get(string username);
        Task<bool> SetAs(OrderState orderState);
    }
}
