using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<AttributeModel> Attributes { get; set; }
    }

    public class CategoryCreateModel
    {
        [Required]
        public string Name { get; set; }

        public IList<AttributeIdModel> Attributes { get; set; }

        public void DistinctAttributesIds()
        {
            if (Attributes != null && Attributes.Count > 1)
            {
                Attributes = Attributes
                    .Select(x => x.Id)
                    .Distinct()
                    .Select(x =>
                        new AttributeIdModel
                        {
                            Id = x
                        }
                    ).ToList();
            }
        }
    }

    public class CategoryUpdateModel
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }

        public IList<AttributeIdModel> Attributes { get; set; }

        public void DistinctAttributesIds()
        {
            if (Attributes != null && Attributes.Count > 1)
            {
                Attributes = Attributes
                    .Select(x => x.Id)
                    .Distinct()
                    .Select(x =>
                        new AttributeIdModel
                        {
                            Id = x
                        }
                    ).ToList();
            }
        }
    }
}
