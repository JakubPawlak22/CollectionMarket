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
        public static string RegisterEndpoint = $"{UsersEndpoint}register/";
        public static string LoginEndpoint = $"{UsersEndpoint}login/";
        public static string MessagesEndpoint = $"{BaseUrl}api/messages/";
        public static string CategoriesEndpoint = $"{BaseUrl}api/categories/";
        public static string AttributesEndpoint = $"{BaseUrl}api/attributes/";
    }
}
