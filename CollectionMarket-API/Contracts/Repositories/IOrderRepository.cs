using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.Repositories
{
    public interface IOrderRepository : IRepositoryBase<Order>
    {
        Task<IList<Order>> GetSoldBy(string username);
        Task<IList<Order>> GetBoughtBy(string username);
        Task<IList<Order>> GetCart(string username);
        Task<Order> GetOrderFromCart(User seller, User buyer);
    }
}
