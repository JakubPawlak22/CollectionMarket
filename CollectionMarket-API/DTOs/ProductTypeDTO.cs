using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_API.DTOs
{
    public class ProductTypeDTO
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueDTO> Attributes { get; set; }
    }
    public class ProductTypeCreateDTO
    {
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public string Name { get; set; }
        public virtual IList<AttributeValueDTO> AttributeValues { get; set; }
    }
    public class ProductTypeUpdateDTO
    {
        public int Id { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public virtual IList<AttributeValueDTO> AttributeValues { get; set; }
    }
}
