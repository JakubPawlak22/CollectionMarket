using Blazored.LocalStorage;
using CollectionMarket_UI.Contracts;
using CollectionMarket_UI.Models;
using CollectionMarket_UI.Providers;
using CollectionMarket_UI.Static;
using Microsoft.AspNetCore.Components.Authorization;
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
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IHttpClientFactory _clientFactory;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public AuthenticationRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider,
            IHttpRequestMessageSender sender)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }

        public async Task<bool> Login(LoginModel model)
        {
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Post,
                Endpoints.LoginEndpoint, model);
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                return false;
            }
            var content = await response.Content.ReadAsStringAsync();
            var token = JsonConvert.DeserializeObject<TokenResponse>(content);

            await _localStorage.SetItemAsync("authToken", token.Token);
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LogIn();

            client.DefaultRequestHeaders.Authorization = 
                new AuthenticationHeaderValue("bearer", token.Token);

            return true;
        }

        public async Task Logout()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).LogOut();
        }

        public async Task<bool> Register(RegistrationModel model)
        {
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Post,
                Endpoints.RegisterEndpoint, model);
            HttpResponseMessage response = await _sender.Send(request);
            return response.IsSuccessStatusCode;
        }
    }
}
