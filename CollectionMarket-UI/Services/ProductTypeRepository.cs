using Blazored.LocalStorage;
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
    public class ProductTypeRepository : IProductTypeRepository
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public ProductTypeRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            IHttpRequestMessageSender sender)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }
        public async Task<bool> Create(string url, ProductTypeCreateModel model)
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

        public async Task<ProductTypeModel> Get(string url, int id)
        {
            if (id < 1)
                return null;
            var request = _director.CreateRequest(HttpMethod.Get, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<ProductTypeModel>(content);
            }
            return null;
        }

        public async Task<IList<ProductTypeModel>> Get(string url)
        {
            return await Get(url, new ProductTypeFilters());
        }

        public async Task<IList<ProductTypeModel>> Get(string url, ProductTypeFilters filters)
        {
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Get, url, filters);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<ProductTypeModel>>(content);
            }
            return null;
        }

        public async Task<bool> Update(string url, ProductTypeUpdateModel model, int id)
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
