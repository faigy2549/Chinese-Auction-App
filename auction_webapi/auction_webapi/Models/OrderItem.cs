using System.Text.Json.Serialization;

namespace auction_webapi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }

        public Order Order { get; set; }
        public int PresentId { get; set; }
  
        public Present Present { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }

        public int Quantity { get; set; } 

        public bool Status { get; set; } 
    }
}
