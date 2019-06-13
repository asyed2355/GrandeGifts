// Added namespaces:
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeGifts.Models
{
    public class Hamper
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HamperId { get; set; }
        [Required]
        public string HamperName { get; set; }
        [Required]
        public double Price { get; set; }
        [Required]
        public string Products { get; set; }
        public string Description { get; set; }
        public string Supplier { get; set; }
        public string ImageUrl { get; set; }
        public bool InUse { get; set; }

        // Foreign Key/s
        public int CategoryId { get; set; }
    }
}
