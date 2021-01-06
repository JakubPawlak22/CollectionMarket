using Blazored.LocalStorage;
using CollectionMarket_UI.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public BaseRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            IHttpRequestMessageSender sender)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }

        public async Task<bool> Create(string url, T obj)
        {
            if (obj == null)
                return false;
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Post, url, obj);
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

        public async Task<T> Get(string url, int id)
        {
            if (id < 1)
                return null;
            var request = _director.CreateRequest(HttpMethod.Get, url + id);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(content);
            }
            return null;
        }

        public async Task<IList<T>> Get(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<T>>(content);
            }
            return null;
        }

        public async Task<bool> Update(string url, T obj)
        {
            if (obj == null)
                return false;
            var request = _director.CreateRequest(HttpMethod.Put, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }
    }
}
