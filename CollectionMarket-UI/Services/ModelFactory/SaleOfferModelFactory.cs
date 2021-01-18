using CollectionMarket_UI.Contracts.ModelFactory;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services.ModelFactory
{
    public class SaleOfferModelFactory : ISaleOfferModelFactory
    {
        public SaleOfferUpdateModel CreateUpdateModel(SaleOfferModel info)
        {
            var model = new SaleOfferUpdateModel
            {
                Condition = info.Condition,
                Count = info.Count,
                Description = info.Description,
                Id = info.Id,
                PricePerItem = info.PricePerItem
            };
            return model;
        }
    }
}
