using CollectionMarket_API.DTOs;
using CollectionMarket_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts
{
    public interface ICategoryService
    {
        Task<CreateObjectResult> Create(CategoryCreateDTO categoryDTO);
        Task<bool> Update(CategoryUpdateDTO categoryDTO);
        Task<CategoryDTO> Get(int id);
        Task<IList<CategoryDTO>> GetAll();
        Task<bool> Delete(int id);
        Task<bool> Exists(int id);
    }
}
