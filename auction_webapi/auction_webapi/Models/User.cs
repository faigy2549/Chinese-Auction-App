using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace auction_webapi.Models
{
    public class User
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
        [MaxLength(30)]

        public string Email { get; set; }
        public EnumRoles Role { get; set; }
        public ICollection<Order> Orders { get; set; }


    }
}
