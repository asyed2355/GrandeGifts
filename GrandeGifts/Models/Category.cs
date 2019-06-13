using System.Collections.Generic;
// Added namespaces:
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeGifts.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        [Required]
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public bool InUse { get; set; }

        //Foreign Key:
        public IEnumerable<Hamper> HamperList { get; set; }
    }
}
