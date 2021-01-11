using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class AttributeValueDTO
    {
        [Required]
        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public DataTypes DataType { get; set; }
        [Required]
        public string AttributeValue { get; set; }
    }
}
