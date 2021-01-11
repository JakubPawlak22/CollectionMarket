using CollectionMarket_API.Filters;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IProductTypeRepository
    {
        Task<bool> Create(string url, ProductTypeCreateModel model);
        Task<bool> Delete(string url, int id);
        Task<ProductTypeModel> Get(string url, int id);
        Task<IList<ProductTypeModel>> Get(string url);
        Task<IList<ProductTypeModel>> Get(string url, ProductTypeFilters filters);
        Task<bool> Update(string url, ProductTypeUpdateModel model, int id);
    }
}
