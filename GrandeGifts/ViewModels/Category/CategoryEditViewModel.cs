// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Category
{
    public class CategoryEditViewModel
    {
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
