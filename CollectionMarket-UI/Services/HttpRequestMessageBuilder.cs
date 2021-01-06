using CollectionMarket_UI.Contracts;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services
{
    public class HttpRequestMessageBuilder : IHttpRequestMessageBuilder
    {
        private HttpRequestMessage _request;

        public IHttpRequestMessageBuilder Init()
        {
            _request = new HttpRequestMessage();
            return this;
        }

        public HttpRequestMessage Build()
        {
            return _request;
        }

        public IHttpRequestMessageBuilder SerializeAsStringContent(object obj)
        {
            _request.Content = new StringContent(JsonConvert.SerializeObject(obj),
                Encoding.UTF8,
                "application/json");
            return this;
        }

        public IHttpRequestMessageBuilder SetMethod(HttpMethod method)
        {
            _request.Method = method;
            return this;
        }

        public IHttpRequestMessageBuilder SetUrl(string url)
        {
            _request.RequestUri = new Uri(url);
            return this;
        }
    }
}
