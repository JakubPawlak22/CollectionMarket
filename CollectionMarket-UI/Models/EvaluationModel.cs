using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CollectionMarket_UI.Models
{
    public class EvaluationModel
    {
        [Required]
        public string EvaluationDescription { get; set; }

        [Required]
        [Range(1, 5)]
        public int Evaluation { get; set; }
    }
}
