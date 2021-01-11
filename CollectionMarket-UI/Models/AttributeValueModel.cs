using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class AttributeValueModel
    {
        [Required]
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        [Required]
        public DataTypes DataType { get; set; }
        [Required]
        public string AttributeValue { get; set; }
    }
    public class AttributeValueEditFormModel
    {
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public DataTypes DataType { get; set; }
        public string TextAttributeValue { get; set; }
        public DateTime? DateAttributeValue { get; set; }
        public double? NumberAttributeValue { get; set; }
        public bool? BooleanAttributeValue { get; set; }

        public string RetrieveAttributeValue()
        {
            string attributeValue = string.Empty;
            switch (DataType)
            {
                case DataTypes.Number:
                    attributeValue = NumberAttributeValue.ToString();
                    break;
                case DataTypes.Text:
                    attributeValue = TextAttributeValue;
                    break;
                case DataTypes.Date:
                    attributeValue = DateAttributeValue.HasValue
                        ? DateAttributeValue.Value.Date.ToString() : string.Empty;
                    break;
                case DataTypes.Boolean:
                    attributeValue = BooleanAttributeValue.Value ? "True" : "False";
                    break;
            }
            return attributeValue;
        }

        public void SetAttributeValue(string value)
        {
            if (!string.IsNullOrEmpty(value))
                switch (DataType)
                {
                    case DataTypes.Number:
                        double number;
                        if (Double.TryParse(value, out number))
                            NumberAttributeValue = number;
                        break;
                    case DataTypes.Text:
                        TextAttributeValue = value;
                        break;
                    case DataTypes.Date:
                        DateTime date;
                        if (DateTime.TryParse(value, out date))
                            DateAttributeValue = date;
                        break;
                    case DataTypes.Boolean:
                        if (value.ToLower().Equals("true"))
                            BooleanAttributeValue = true;
                        if (value.ToLower().Equals("false"))
                            BooleanAttributeValue = false;
                        break;
                }
        }
    }
}
