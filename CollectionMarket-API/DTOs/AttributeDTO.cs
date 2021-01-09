using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class AttributeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DataTypes? DataType { get; set; }
    }
    public class AttributeCreateDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DataTypes DataType { get; set; }
    }
    public class AttributeUpdateDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DataTypes DataType { get; set; }
    }
    public class AttributeIdDTO
    {
        [Required]
        public int Id { get; set; }
    }
}
