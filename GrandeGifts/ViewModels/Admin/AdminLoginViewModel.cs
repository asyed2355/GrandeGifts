// Added namespaces:
using System.ComponentModel.DataAnnotations;

namespace GrandeGifts.ViewModels.Admin
{
    public class AdminLoginViewModel
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string ReturnUrl { get; set; }
    }
}
