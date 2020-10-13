using Newtonsoft.Json;

namespace BizActionExample.Domain.Models.Accounts
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string Password { get; set; }
        public string HashSalt { get; set; }
        public int Refno { get; set; }
        public UserType UserType { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}