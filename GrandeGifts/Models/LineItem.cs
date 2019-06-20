using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.Models
{
    public class LineItem
    {
        public Guid OrderId { get; set; }
        public int HamperId { get; set; }
        public int Quantity  { get; set; }
    }
}
