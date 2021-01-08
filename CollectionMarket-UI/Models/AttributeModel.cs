using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class AttributeModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public DataTypes? DataType { get; set; }
    }
}
