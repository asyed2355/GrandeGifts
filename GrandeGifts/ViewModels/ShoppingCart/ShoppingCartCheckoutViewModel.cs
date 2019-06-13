using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Added namespaces:
using GrandeGifts.Models;

namespace GrandeGifts.ViewModels.ShoppingCart
{
    public class ShoppingCartCheckoutViewModel
    {
        // User details:
        public GrandeGifts.Models.Address PreferredAddress { get; set; }

        // Shopping Cart Items:
        public double PriceLessDelivery { get; set; }
        public static double DeliveryFee
        {
            get
            {
                return 7.50;
            }
        }
        public List<ShoppingCartItem> shoppingCartItems;        
    }
}
