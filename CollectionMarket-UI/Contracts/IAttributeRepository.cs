using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface IAttributeRepository : IBaseRepository<AttributeModel>
    {
        Task<IList<AttributeModel>> GetCategoryAttributes(string url, int categoryId);
    }
}
