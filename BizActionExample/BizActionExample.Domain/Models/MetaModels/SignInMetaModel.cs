using System.ComponentModel.DataAnnotations;

namespace BizActionExample.Domain.Models.MetaModels
{
    public class SignInMetaModel
    {
        public bool IsTokenRequest { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Password { get; set; }
    }
}