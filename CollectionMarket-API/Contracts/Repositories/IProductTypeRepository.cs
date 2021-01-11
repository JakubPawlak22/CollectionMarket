using CollectionMarket_API.Data;
using CollectionMarket_API.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.Repositories
{
    public interface IProductTypeRepository : IRepositoryFiltered<ProductType, ProductTypeFilters>
    {
    }
}
