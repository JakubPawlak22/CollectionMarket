using CollectionMarket_API.DTOs;
using CollectionMarket_API.Filters;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IProductTypeService
    {
        Task<CreateObjectResult> Create(ProductTypeCreateDTO productTypeDTO);
        Task<bool> Update(ProductTypeUpdateDTO productTypeDTO);
        Task<ProductTypeDTO> Get(int id);
        Task<IList<ProductTypeDTO>> GetFiltered(ProductTypeFilters filters);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
