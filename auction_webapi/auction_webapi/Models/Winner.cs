using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace auction_webapi.Models
{
    public class Winner
    {
        [Key, NotNull]
        public int Id { get; set; }

        public int PresentId { get; set; }
        [JsonIgnore]
        public Present Present { get; set; }


        public int UserId { get; set; }
        [JsonIgnore]
        public User User { get; set; }


        public int RaffleId { get; set; }
        [JsonIgnore]
        public string Email { get; set; }
        public Raffle Raffle { get; set; }
    }
}
