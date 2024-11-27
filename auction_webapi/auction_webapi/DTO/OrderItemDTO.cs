using auction_webapi.Models;
using System.Text.Json.Serialization;

namespace auction_webapi.DTO
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        public int? OrderId { get; set; }
        public int PresentId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public bool Status { get; set; }
    }
}
