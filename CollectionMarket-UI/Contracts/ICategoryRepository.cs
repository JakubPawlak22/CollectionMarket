using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts
{
    public interface ICategoryRepository
    {
         Task<bool> Create(string url, CategoryCreateModel model);
         Task<bool> Delete(string url, int id);
         Task<CategoryModel> Get(string url, int id);
         Task<IList<CategoryModel>> Get(string url);
         Task<bool> Update(string url, CategoryUpdateModel model, int id);
    }
}
