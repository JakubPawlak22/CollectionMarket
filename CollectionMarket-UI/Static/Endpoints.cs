﻿using System;
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
        public static string AddToCartEndpoint = $"{SaleOffersEndpoint}addtocart/";
        public static string RemoveFromCartEndpoint = $"{SaleOffersEndpoint}removefromcart/";
        public static string OrdersEndpoint = $"{BaseUrl}api/orders/";
        public static string MakeOrderEndpoint = $"{OrdersEndpoint}makeorder/";
        public static string CartEndpoint = $"{OrdersEndpoint}cart/";
        public static string SentEndpoint = $"{OrdersEndpoint}sent/";
        public static string LostEndpoint = $"{OrdersEndpoint}lost/";
        public static string DeliveredEndpoint = $"{OrdersEndpoint}delivered/";
        public static string EvaluationEndpoint = $"{OrdersEndpoint}evaluation/";
        public static string SoldOrdersEndpoint = $"{OrdersEndpoint}soldorders/";
        public static string BoughtOrdersEndpoint = $"{OrdersEndpoint}boughtorders/";
    }
}
