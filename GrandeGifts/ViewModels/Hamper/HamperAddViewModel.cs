using System.Collections.Generic;
//Added namespaces:
using GrandeGifts.Models;
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Hamper
{
    public class HamperAddViewModel
    {
        [Required]
        public string HamperName { get; set; }
        [Range(0,1000)]
        public double Price { get; set; }
        [Required]
        public string Products { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string ImageUrl { get; set; }
        public bool InUse { get; set; }
        // Foreign Key:
        //public GrandeGifts.Models.Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
