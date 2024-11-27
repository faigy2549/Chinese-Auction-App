using auction_webapi.Models;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace auction_webapi.DTO
{
    public class OrderDTO
    {
        [Key, NotNull]
        public int OrderId { get; set; }
        public int UserId { get; set; }

        public DateTime Date { get; set; }
        public int Sum { get; set; }
        public ICollection<OrderItemDTO> OrderItems { get; set; }
    }
}
