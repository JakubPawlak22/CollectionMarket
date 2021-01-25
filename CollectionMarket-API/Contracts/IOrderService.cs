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
        Task<IList<OrderDTO>> GetSoldBy(string username);
        Task<IList<OrderDTO>> GetBoughtBy(string username);
        Task<bool> AddEvaluation(int id, EvaluationDTO evaluation, string username);
        Task<bool> RemoveFromCart(int saleOfferId, string buyerUsername);
        Task<bool> AddToCart(int saleOfferId, string buyerUsername);
        Task<bool> SetAsOrdered(int id);
        Task<bool> SetAsSent(int id);
        Task<bool> SetAsLost(int id);
        Task<bool> SetAsDelivered(int id);
    }
}
