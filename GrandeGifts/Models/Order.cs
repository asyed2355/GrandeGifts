using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.Models
{
    public class Order
    {
        public Guid OrderId { get; set; }
        public double Price { get; set; }
        public DateTime DateOrdered { get; set; }

        // Address:
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }
        
        // Foreign Key:
        public string UserId { get; set; }

        // Product List:
        public List<LineItem> ShoppingCartItems { get; set; }
    }
}
