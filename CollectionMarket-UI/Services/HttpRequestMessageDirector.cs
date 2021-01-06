using CollectionMarket_UI.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class HttpRequestMessageDirector
    {
        private IHttpRequestMessageBuilder _builder;
        public IHttpRequestMessageBuilder Builder 
        { 
            set { _builder = value; } 
        }

        public HttpRequestMessage CreateRequest(HttpMethod method, string url)
        {
            var request = _builder
                .Init()
                .SetMethod(method)
                .SetUrl(url)
                .Build();
            return request;
        }

        public HttpRequestMessage CreateRequestWithSerializedObject(HttpMethod method, string url, object obj)
        {
            var request = _builder
                .Init()
                .SetMethod(method)
                .SetUrl(url)
                .SerializeAsStringContent(obj)
                .Build();
            return request;
        }
    }
}
