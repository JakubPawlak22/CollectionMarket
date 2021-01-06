using Blazored.LocalStorage;
using CollectionMarket_UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class RequestWithBearerTokenSender : IHttpRequestMessageSender
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        public RequestWithBearerTokenSender(ILocalStorageService localStorage,
            IHttpClientFactory clientFactory)
        {
            _localStorage = localStorage;
            _clientFactory = clientFactory;
        }
        public async Task<HttpResponseMessage> Send(HttpRequestMessage request)
        {
            var client = _clientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("bearer", await GetBearerToken());
            HttpResponseMessage response = await client.SendAsync(request);
            return response;
        }

        private async Task<string> GetBearerToken()
        {
            return await _localStorage.GetItemAsync<string>("authToken");
        }
    }
}
