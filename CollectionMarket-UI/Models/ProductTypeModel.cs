using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class ProductTypeModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueModel> Attributes { get; set; }
    }
    public class ProductTypeCreateModel
    {
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueModel> AttributeValues { get; set; }
    }
    public class ProductTypeUpdateModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueModel> AttributeValues { get; set; }
    }
    public class ProductTypeEditFormModel
    {
        public int Id { get; set; }
        public int? CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueEditFormModel> AttributeValues { get; set; }
    }
}
