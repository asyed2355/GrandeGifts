using System;
using System.Collections.Generic;
// Added namespaces:
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [MaxLength(128)]
        public string GivenNames { get; set; }

        [Required]
        [MaxLength(128)]
        public string Surname { get; set; }

        public DateTime JoinDate { get; set; }

        // Foreign Key
        public List<Address> Addresses { get; set; }
    }
}
