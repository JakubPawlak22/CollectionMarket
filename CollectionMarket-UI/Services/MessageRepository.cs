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
    public class MessageRepository : IMessageRepository
    {
        private readonly IHttpRequestMessageSender _sender;
        private HttpRequestMessageDirector _director;
        public MessageRepository(IHttpRequestMessageSender sender)
        {
            _sender = sender;
            _director = new HttpRequestMessageDirector();
            _director.Builder = new HttpRequestMessageBuilder();
        }
        public async Task<bool> Create(string url, MessageCreateModel model)
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

        public async Task<IList<MessageModel>> GetConversation(string url, string username)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url + username);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<MessageModel>>(content);
            }
            return null;
        }

        public async Task<IList<ConversationModel>> GetConversations(string url)
        {
            var request = _director.CreateRequest(HttpMethod.Get, url);
            HttpResponseMessage response = await _sender.Send(request);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IList<ConversationModel>>(content);
            }
            return null;
        }
    }
}
