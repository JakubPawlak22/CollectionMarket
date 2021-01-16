using CollectionMarket_UI.Filters;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface ISaleOfferRepository
    {
        Task<bool> Create(string url, SaleOfferCreateModel model);
        Task<bool> Delete(string url, int id);
        Task<SaleOfferModel> Get(string url, int id);
        Task<IList<SaleOfferModel>> Get(string url);
        Task<bool> Update(string url, SaleOfferUpdateModel model, int id);
        Task<IList<SaleOfferModel>> Get(string url, SaleOfferFilters filters);
    }
}
