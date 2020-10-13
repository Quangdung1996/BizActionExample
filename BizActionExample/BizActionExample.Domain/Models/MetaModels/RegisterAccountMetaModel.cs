using System.ComponentModel.DataAnnotations;

namespace BizActionExample.Domain.Models.MetaModels
{
    public class RegisterAccountMetaModel
    {
        [Required]
        public int Refno { get; set; }

        [Required]
        public UserType UserType { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

    }
}