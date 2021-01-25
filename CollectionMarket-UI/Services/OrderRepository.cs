using CollectionMarket_UI.Contracts;
using CollectionMarket_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public OrderRepository(IHttpRequestMessageSender sender)
        {
            _sender = sender;
            _director = new HttpRequestMessageDirector
            {
                Builder = new HttpRequestMessageBuilder()
            };
        }

        public async Task<bool> AddEvaluation(string url, int id, EvaluationModel evaluation)
        {
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Post, url + id, evaluation);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeState(string url, int id)
        {
            var request = _director.CreateRequest(HttpMethod.Post, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<IList<OrderModel>> GetLoggedUserBoughtOrders(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<OrderModel>>(content);
            }
            return null;
        }

        public async Task<IList<OrderModel>> GetLoggedUserCart(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<OrderModel>>(content);
            }
            return null;
        }

        public async Task<IList<OrderModel>> GetLoggedUserSoldOrders(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<OrderModel>>(content);
            }
            return null;
        }

        public async Task<OrderModel> GetOrderById(string url, int id)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<OrderModel>(content);
            }
            return null;
        }
    }
}
