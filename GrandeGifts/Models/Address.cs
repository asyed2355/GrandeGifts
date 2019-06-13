using System;
// Added namespaces:
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GrandeGifts.Models
{
    public class Address
    {
        [Key]
        public Guid AddressId { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Suburb { get; set; }
        public string State { get; set; }
        public int Postcode { get; set; }
        public string AddressType { get; set; }
        public bool PreferredShippingAddress { get; set; }
        // Foreign Key:
        public string ApplicationUserId { get; set; }
    }
}
