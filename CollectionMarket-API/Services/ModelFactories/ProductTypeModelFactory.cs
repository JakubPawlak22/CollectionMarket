using CollectionMarket_API.Contracts.ModelFactories;
using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.ModelFactories
{
    public class ProductTypeModelFactory : IProductTypeModelFactory
    {
        public ProductType CreateEntity(ProductTypeCreateDTO productTypeDTO)
        {
            var product = new ProductType
            {
                CategoryId = productTypeDTO.CategoryId,
                Name = productTypeDTO.Name,
                AttributeValues = productTypeDTO.AttributeValues.Select(x => new AttributeValue
                {
                    AttributeId = x.AttributeId,
                    Value = x.AttributeValue
                }).ToList()
            };
            return product;
        }

        public ProductType CreateEntity(ProductTypeUpdateDTO productTypeDTO)
        {
            var product = new ProductType
            {
                Id = productTypeDTO.Id,
                CategoryId = productTypeDTO.CategoryId,
                Name = productTypeDTO.Name,
                AttributeValues = productTypeDTO.AttributeValues.Select(x => new AttributeValue
                {
                    AttributeId = x.AttributeId,
                    Value = x.AttributeValue
                }).ToList()
            };
            return product;
        }
    }
}

