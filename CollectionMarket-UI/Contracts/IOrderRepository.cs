using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IOrderRepository
    {
        public Task<IList<OrderModel>> GetLoggedUserSoldOrders(string url);
        public Task<IList<OrderModel>> GetLoggedUserBoughtOrders(string url);
        public Task<IList<OrderModel>> GetLoggedUserCart(string url);
        public Task<OrderModel> GetOrderById(string url, int id);
        public Task<bool> ChangeState(string url, int id);
        public Task<bool> AddEvaluation(string url, int id, EvaluationModel evaluation);
    }
}
