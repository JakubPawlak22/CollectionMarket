using CollectionMarket_UI.Contracts.ModelFactory;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services.ModelFactory
{
    public class AttributeValueModelFactory : IAttributeValueModelFactory
    {
        public IList<AttributeValueEditFormModel> CreateAttributeValueModels(IList<AttributeModel> attributes)
        {
            var attributeValues = attributes.Select(x =>
                new AttributeValueEditFormModel
                {
                    AttributeId = x.Id,
                    AttributeName = x.Name,
                    DataType = x.DataType.Value
                }
            ).ToList();
            return attributeValues;
        }
    }
}
