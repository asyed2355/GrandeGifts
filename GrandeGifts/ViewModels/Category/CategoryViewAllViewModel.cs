using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.Category
{
    public class CategoryViewAllViewModel
    {
        public int Total { get; set; }
        public IEnumerable<GrandeGifts.Models.Category> Categories { get; set; }
    }
}
