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
    public class AttributeRepository : BaseRepository<AttributeModel>, IAttributeRepository
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public AttributeRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            IHttpRequestMessageSender sender) : base(clientFactory, localStorage, sender)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }

        public async Task<IList<AttributeModel>> GetCategoryAttributes(string url, int categoryId)
        {
            var filters = new AttributeFilters
            {
                CategoryId = categoryId
            };
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Get, url, filters);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<AttributeModel>>(content);
            }
            return null;
        }

        public async override Task<IList<AttributeModel>> Get(string url)
        {
            AttributeFilters attributeFilters = new AttributeFilters();
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Get, url, attributeFilters);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<AttributeModel>>(content);
            }
            return null;
        }
    }
}
