// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Admin
{
    public class AdminRegisterAdminViewModel
    {
        [Required, MaxLength(128)]
        public string GivenNames { get; set; }
        [Required, MaxLength(128)]
        public string Surname { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        [Required, DataType(DataType.Password), Compare(nameof(Password), ErrorMessage = "The passwords you entered don't match.")]
        public string ComparePassword { get; set; }


        // --Address Fields-- //
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Suburb { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public int Postcode { get; set; }
    }
}
