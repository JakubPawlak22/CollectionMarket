using CollectionMarket_UI.Contracts;
using CollectionMarket_UI.Filters;
using CollectionMarket_UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class SaleOfferRepository : ISaleOfferRepository
    {
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public SaleOfferRepository(IHttpRequestMessageSender sender)
        {
            _sender = sender;
            _director = new HttpRequestMessageDirector
            {
                Builder = new HttpRequestMessageBuilder()
            };
        }

        public async Task<bool> Create(string url, SaleOfferCreateModel model)
        {
            if (model == null)
                return false;
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Post, url, model);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(string url, int id)
        {
            if (id < 1)
                return false;
            var request = _director.CreateRequest(HttpMethod.Delete, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<SaleOfferModel> Get(string url, int id)
        {
            if (id < 1)
                return null;
            var request = _director.CreateRequest(HttpMethod.Get, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SaleOfferModel>(content);
            }
            return null;
        }

        public async Task<IList<SaleOfferModel>> Get(string url)
        {
            return await Get(url, null);
        }

        public async Task<IList<SaleOfferModel>> Get(string url, SaleOfferFilters filters)
        {
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Get, url, filters);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<SaleOfferModel>>(content);
            }
            return null;
        }

        public async Task<bool> Update(string url, SaleOfferUpdateModel model, int id)
        {
            if (model == null)
                return false;
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Put, url + id, model);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }
    }
}
