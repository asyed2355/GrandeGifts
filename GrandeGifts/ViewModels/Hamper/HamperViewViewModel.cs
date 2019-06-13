using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.Hamper
{
    public class HamperViewViewModel
    {
        public int HamperId { get; set; }
        public string HamperName { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
    }
}
