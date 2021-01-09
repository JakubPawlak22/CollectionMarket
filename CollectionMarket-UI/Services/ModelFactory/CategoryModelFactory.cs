using CollectionMarket_UI.Contracts.ModelFactory;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services.ModelFactory
{
    public class CategoryModelFactory : ICategoryModelFactory
    {
        public CategoryUpdateModel CreateUpdateModel(CategoryModel info)
        {
            var model = new CategoryUpdateModel
            {
                Id = info.Id,
                Name = info.Name,
                Attributes = info.Attributes
                .Select(x => x.Id)
                .Distinct()
                .Select(x => new AttributeIdModel
                {
                    Id = x
                }).ToList()
            };
            model.Attributes ??= new List<AttributeIdModel>();
            return model;
        }
    }
}
