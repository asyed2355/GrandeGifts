using System.Collections.Generic;
//Added namespaces:
using System.ComponentModel.DataAnnotations;


namespace GrandeGifts.ViewModels.Category
{
    public class CategoryManageViewModel
    {
        public int Total { get; set; }
        public int InUse { get; set; }
        public int NotInUse { get; set; }
        public IEnumerable<GrandeGifts.Models.Category> Categories { get; set; }
    }
}
