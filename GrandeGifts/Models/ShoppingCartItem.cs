using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Added namespaces:
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeGifts.Models
{
    public class ShoppingCartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ShoppingCartItemId { get; set; }
        public int Quantity { get; set; }
        //Foreign Key:
        public Hamper Hamper { get; set; }
    }
}
