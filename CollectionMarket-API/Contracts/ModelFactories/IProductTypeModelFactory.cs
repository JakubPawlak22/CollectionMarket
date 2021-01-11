using CollectionMarket_API.Data;
using CollectionMarket_API.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Contracts.ModelFactories
{
    public interface IProductTypeModelFactory
    {
        ProductType CreateEntity(ProductTypeCreateDTO productTypeDTO);
        ProductType CreateEntity(ProductTypeUpdateDTO productTypeDTO);
    }
}
