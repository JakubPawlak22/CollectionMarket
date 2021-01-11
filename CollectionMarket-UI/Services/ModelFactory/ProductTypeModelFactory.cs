using CollectionMarket_UI.Contracts.ModelFactory;
using CollectionMarket_UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Services.ModelFactory
{
    public class ProductTypeModelFactory : IProductTypeModelFactory
    {
        public ProductTypeCreateModel CreateCreateModel(ProductTypeEditFormModel info)
        {
            ProductTypeCreateModel model = new ProductTypeCreateModel
            {
                CategoryId = info.CategoryId,
                Name = info.Name,
                AttributeValues = CreateAttributeValueModels(info.AttributeValues)
            };
            return model;
        }

        private IList<AttributeValueModel> CreateAttributeValueModels(IList<AttributeValueEditFormModel> editFormModels)
        {
            var models = editFormModels.Select(x => new AttributeValueModel
            {
                AttributeId = x.AttributeId,
                AttributeName = x.AttributeName,
                DataType = x.DataType,
                AttributeValue = x.RetrieveAttributeValue()
            }).ToList();
            return models;
        }

        public ProductTypeEditFormModel CreateEditFormModel(ProductTypeModel info)
        {
            var model = new ProductTypeEditFormModel()
            {
                Id = info.Id,
                CategoryId = info.CategoryId,
                Name = info.Name,
                AttributeValues = PrepareAttValueEditModels(info.Attributes)
            };
            return model;
        }

        private List<AttributeValueEditFormModel> PrepareAttValueEditModels(IList<AttributeValueModel> attributes)
        {
            var models = new List<AttributeValueEditFormModel>();
            foreach (var att in attributes)
            {
                var attModel = CreateAttValueEditModel(att);
                models.Add(attModel);
            }
            return models;
        }

        private AttributeValueEditFormModel CreateAttValueEditModel(AttributeValueModel info)
        {
            var attModel = new AttributeValueEditFormModel
            {
                AttributeId = info.AttributeId,
                AttributeName = info.AttributeName,
                DataType = info.DataType
            };
            attModel.SetAttributeValue(info.AttributeValue);
            return attModel;
        }

        public ProductTypeUpdateModel CreateUpdateModel(ProductTypeEditFormModel info)
        {
            ProductTypeUpdateModel model = new ProductTypeUpdateModel
            {
                Id = info.Id,
                CategoryId = info.CategoryId,
                Name = info.Name,
                AttributeValues = CreateAttributeValueModels(info.AttributeValues)
            };
            return model;
        }
    }
}
