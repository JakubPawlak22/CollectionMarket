using CollectionMarket_API.Contracts.Validators;
using CollectionMarket_API.Data;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Services.Validators
{
    public class ProductTypeWithAttributesValidator : BaseValidator<ProductType>, IProductTypeWithAttributesValidator
    {
        private readonly ApplicationDbContext _context;
        public ProductTypeWithAttributesValidator(ApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Valid()
        {
            await ValidateAttributeList();
        }

        private async Task ValidateAttributeList()
        {
            var attributes = _context.CategoryAttributes
                .Where(x => x.CategoryId == _entity.CategoryId)
                .Select(x => x.Attribute)
                .ToList();
            if (attributes.Count != _entity.AttributeValues.Count)
                AddError($"There are {_entity.AttributeValues.Count} Attribute Values instead of {attributes.Count}");
            else
                foreach (var value in _entity.AttributeValues)
                {
                    var attribute = attributes.SingleOrDefault(x => x.Id == value.AttributeId);
                    if (attribute == null)
                    {
                        AddError($"Wrong Attribute");
                    }
                    else
                    {
                        ValidateAttributeValueDataType(value, (DataTypes)attribute.DataType);
                    }
                }

        }

        private void ValidateAttributeValueDataType(AttributeValue value, DataTypes dataType)
        {
            switch (dataType)
            {
                case DataTypes.Number:
                    ValidateNumberAttribute(value);
                    break;
                case DataTypes.Text:
                    break;
                case DataTypes.Boolean:
                    ValidateBooleanAttribute(value);
                    break;
                case DataTypes.Date:
                    ValidateDateAttribute(value);
                    break;
            }
        }

        private void ValidateNumberAttribute(AttributeValue value)
        {
            double tempNumber;
            if (!Double.TryParse(value.Value, out tempNumber))
                AddError($"{value.Value} is not a Number");
        }

        private void ValidateBooleanAttribute(AttributeValue value)
        {
            var isBoolean = value.Value.ToLower().Equals("true") || value.Value.ToLower().Equals("false");
            if (!isBoolean)
                AddError($@"{value.Value} is not ""true"" or ""false""");
        }

        private void ValidateDateAttribute(AttributeValue value)
        {
            DateTime tempDate;
            if (!DateTime.TryParse(value.Value, out tempDate))
                AddError($"{value.Value} is not a Date");
        }
    }
}
