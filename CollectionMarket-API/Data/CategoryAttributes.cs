using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    [Table("CategoryAttributes")]
    public class CategoryAttributes
    {
        public int CategoryId { get; set; }
        public int AttributeId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category Category { get; set; }
        [ForeignKey("AttributeId")]
        public virtual Attribute Attribute { get; set; }
    }
}
