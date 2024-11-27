using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace auction_webapi.Models
{
    public class Order
    {
            [Key, NotNull]
            public int OrderId { get; set; }
            public int UserId { get; set; }
            public User User { get; set; }
            public DateTime Date { get; set; }
            public double Sum { get; set; }
    }
}