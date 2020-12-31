using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.Repositories
{
    public interface IRepositoryFiltered<TData, TFilters>
        : IRepositoryBase<TData>
        where TData : class
        where TFilters : class
    {
        Task<IList<TData>> GetFiltered(TFilters filters);
    }
}
