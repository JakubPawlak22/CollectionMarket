using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IHttpRequestMessageBuilder
    {
        IHttpRequestMessageBuilder Init();
        HttpRequestMessage Build();
        IHttpRequestMessageBuilder SetMethod(HttpMethod method);
        IHttpRequestMessageBuilder SerializeAsStringContent(object obj);
        IHttpRequestMessageBuilder SetUrl(string url);
    }
}
