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
        public static string ProfileEndpoint = $"{UsersEndpoint}profile/";
        public static string WithdrawEndpoint = $"{UsersEndpoint}withdraw/";
        public static string DepositEndpoint = $"{UsersEndpoint}deposit/";
        public static string MoneyEndpoint = $"{UsersEndpoint}money/";
        public static string MessagesEndpoint = $"{BaseUrl}api/messages/";
        public static string CategoriesEndpoint = $"{BaseUrl}api/categories/";
        public static string AttributesEndpoint = $"{BaseUrl}api/attributes/";
        public static string ProductTypesEndpoint = $"{BaseUrl}api/producttypes/";
        public static string SaleOffersEndpoint = $"{BaseUrl}api/saleoffers/";
    }
}
