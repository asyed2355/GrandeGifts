//Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Hamper
{
    public class HamperEditViewModel
    {
        public int HamperId { get; set; }
        [Required]
        public string HamperName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Products { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string ImageUrl { get; set; }
        // Foreign Key:
        public int CategoryId { get; set; }
    }
}
