using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace auction_webapi.Models
{
    public class Raffle
    {
        [Key,NotNull]
        public int RaffleId { get; set; }
        public DateTime RaffleDate { get; set; }

    }
}
