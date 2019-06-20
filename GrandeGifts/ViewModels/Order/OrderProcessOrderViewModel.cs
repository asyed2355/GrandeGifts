using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.Order
{
    public class OrderProcessOrderViewModel
    {
        // These fields are meant for people who aren't registered.
        public string StreetAddress { get; set; }
        public string Suburb { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }



    }
}
