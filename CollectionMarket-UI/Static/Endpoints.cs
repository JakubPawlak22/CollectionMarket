using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Static
{
    public static class Endpoints
    {
        public static string BaseUrl = "https://localhost:44342/";
        public static string UsersEndpoint = $"{BaseUrl}api/users/";
        public static string RegisterEndpoint = $"{BaseUrl}api/users/register/";
        public static string LoginEndpoint = $"{BaseUrl}api/users/login/";
        public static string MessagesEndpoint = $"{BaseUrl}api/messages/";
    }
}
