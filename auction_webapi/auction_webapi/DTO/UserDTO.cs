using auction_webapi.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace auction_webapi.DTO
{
    public class UserDTO
    {
        [Key, NotNull]
        public int UserId { get; set; }

        [NotNull]
        public string Password { get; set; }

        [NotNull]
        [MaxLength(30)]
        public string FirstName { get; set; }

        [NotNull]
        [MaxLength(30)]
        public string LastName { get; set; }
        [MaxLength(15)]

        public string Email { get; set; }
        public EnumRoles Role { get; set; }
        public ICollection<OrderDTO> Orders { get; set; }
    }
}
