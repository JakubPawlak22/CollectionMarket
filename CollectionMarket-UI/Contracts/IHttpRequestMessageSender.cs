using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IHttpRequestMessageSender
    {
        Task<HttpResponseMessage> Send(HttpRequestMessage request);
    }
}
