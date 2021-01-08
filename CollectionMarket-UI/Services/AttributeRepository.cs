using Blazored.LocalStorage;
using CollectionMarket_UI.Contracts;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class AttributeRepository: BaseRepository<AttributeModel>, IAttributeRepository
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
    }
}
