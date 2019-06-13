using System.Collections.Generic;

namespace GrandeGifts.ViewModels.Home
{
    public class HomeSearchViewModel
    {
        public string Keyword { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public IEnumerable<GrandeGifts.Models.Hamper> Hampers { get; set; }
    }
}
