using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<AttributeDTO> Attributes { get; set; }
    }
    public class CategoryCreateDTO
    {
        [Required]
        public string Name { get; set; }

        public IList<AttributeIdDTO> Attributes { get; set; }
    }
    public class CategoryUpdateDTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<AttributeIdDTO> Attributes { get; set; }
    }
}
