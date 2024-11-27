using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace auction_webapi.DTO
{
    public class UserLoginDTO
    {
        public string Password { get; set; }

        public string Email { get; set; }
    }
}
