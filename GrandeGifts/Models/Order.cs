using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public double Price { get; set; }
        // Foreign Key:
        public IEnumerable<ShoppingCartItem> ShoppingCartItems { get; set; }
    }
}
