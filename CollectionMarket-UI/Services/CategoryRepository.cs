using Blazored.LocalStorage;
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
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public CategoryRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            IHttpRequestMessageSender sender)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }

        public async Task<bool> Create(string url, CategoryCreateModel model)
        {
            if (model == null)
                return false;
            model.DistinctAttributesIds();
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

        public async Task<CategoryModel> Get(string url, int id)
        {
            if (id < 1)
                return null;
            var request = _director.CreateRequest(HttpMethod.Get, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<CategoryModel>(content);
            }
            return null;
        }

        public async Task<IList<CategoryModel>> Get(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<CategoryModel>>(content);
            }
            return null;
        }

        public async Task<bool> Update(string url, CategoryUpdateModel model, int id)
        {
            if (model == null)
                return false;
            model.DistinctAttributesIds();
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
