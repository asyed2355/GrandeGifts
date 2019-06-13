using System.Collections.Generic;
// Added namespaces:
using System.ComponentModel.DataAnnotations;
using GrandeGifts.Models;

namespace GrandeGifts.ViewModels.Account
{
    public class AccountManageProfileViewModel
    {
        [Required]
        public string UserName { get; set; }
        public string GivenNames { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        //Foreign Key
        public List<GrandeGifts.Models.Address> Addresses { get; set; }
    }
}
