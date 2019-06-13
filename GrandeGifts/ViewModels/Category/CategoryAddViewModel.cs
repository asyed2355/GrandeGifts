// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Category
{
    public class CategoryAddViewModel
    {
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
