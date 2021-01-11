using CollectionMarket_API.DTOs;
using CollectionMarket_API.Models;
using CollectionMarket_UI.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface IAttributeService
    {
        Task<CreateObjectResult> Create(AttributeCreateDTO attributeDTO);
        Task<bool> Update(AttributeUpdateDTO attributeDTO);
        Task<AttributeDTO> Get(int id);
        Task<IList<AttributeDTO>> GetFiltered(AttributeFilters filters);
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
