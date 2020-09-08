using System.ComponentModel.DataAnnotations;

namespace BizActionExample.Domain.Models.Accounts
{
    public class RegisterAccount
    {
        [Required]
        public int Refno { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string PasswordConfirmation { get; set; }
    }
}