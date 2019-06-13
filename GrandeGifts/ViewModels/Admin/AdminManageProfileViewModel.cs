// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Admin
{
    public class AdminManageProfileViewModel
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string GivenNames { get; set; }
        [Required]
        public string Surname { get; set; }
        public string PhoneNo { get; set; }

        // Address
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string Suburb { get; set; }
        public string State { get; set; }
        [DataType(DataType.PostalCode)]
        public int Postcode { get; set; }
    }
}
