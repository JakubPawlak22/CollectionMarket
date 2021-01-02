using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.Data
{
    [Table("Attributes")]
    public class Attribute
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual IList<AttributeValue> AttributeValues { get; set; }
        public virtual IList<CategoryAttributes> CategoryAttributes { get; set; }
    }
}
