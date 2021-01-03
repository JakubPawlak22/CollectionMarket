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

        public AuthenticationRepository(IHttpClientFactory clientFactory,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider)
        {
            _clientFactory = clientFactory;
            _localStorage = localStorage;
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> Login(LoginModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.LoginEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(model),
                Encoding.UTF8, "application/json");
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
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LogOut();
        }

        public async Task<bool> Register(RegistrationModel model)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, Endpoints.RegisterEndpoint);
            request.Content = new StringContent(JsonConvert.SerializeObject(model), 
                Encoding.UTF8, "application/json");
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            return response.IsSuccessStatusCode;
        }
    }
}
