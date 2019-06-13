using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GrandeGifts.ViewModels.Home
{
    public class HomeFilterByPriceViewModel
    {
        public string Keyword { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public string CategoryName { get; set; }
        public IEnumerable<GrandeGifts.Models.Hamper> Hampers { get; set; }
    }
}
