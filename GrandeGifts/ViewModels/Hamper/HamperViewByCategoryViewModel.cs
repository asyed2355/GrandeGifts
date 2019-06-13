using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.Hamper
{
    public class HamperViewByCategoryViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public double Price { get; set; }
        public string Products { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string ImageUrl { get; set; }
    }
}
