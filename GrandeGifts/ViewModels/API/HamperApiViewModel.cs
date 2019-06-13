using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.API
{
    public class HamperApiViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public double Price { get; set; }
        public string Products { get; set; }
        public string ImageUrl { get; set; }
        public string CategoryName { get; set; }
    }
}
