using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Contracts.ModelFactory
{
    public interface IAttributeValueModelFactory
    {
        IList<AttributeValueEditFormModel> CreateAttributeValueModels(IList<AttributeModel> attributes);
    }
}
