// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Account
{
    public class AccountRegisterViewModel
    {
        [Required, MaxLength(256)]
        public string UserName { get; set; }
        [Required, MaxLength(100)]
        public string GivenNames { get; set; }
        [Required, MaxLength(100)]
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
        public string State { get; set; }
        [DataType(DataType.PostalCode)]
        public int Postcode { get; set; }
        public string AddressType { get; set; }
        //public bool PreferredShippingAddress { get; set; }
    }
}
