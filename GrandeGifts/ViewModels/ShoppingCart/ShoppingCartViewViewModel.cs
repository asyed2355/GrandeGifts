using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.ShoppingCart
{
    public class ShoppingCartViewViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
