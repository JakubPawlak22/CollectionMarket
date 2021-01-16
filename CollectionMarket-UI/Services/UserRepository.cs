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
    public class UserRepository : IUserRepository
    {
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;

        public UserRepository(IHttpRequestMessageSender sender)
        {
            _sender = sender;
            _director = new HttpRequestMessageDirector
            {
                Builder = new HttpRequestMessageBuilder()
            };
        }

        public async Task<UserProfileModel> GetLoggedUser(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserProfileModel>(content);
            }
            return null;
        }

        public async Task<bool> UpdateLoggedUser(string url, UserProfileModel model)
        {
            if (model == null)
                return false;
            var request = _director.CreateRequestWithSerializedObject(HttpMethod.Patch, url, model);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return true;
            }
            return false;
        }

        public async Task<UserModel> Get(string url, string name)
        {
            if (string.IsNullOrEmpty(name))
                return null;
            var request = _director.CreateRequest(HttpMethod.Get, url + name);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<UserModel>(content);
            }
            return null;
        }
    }
}
