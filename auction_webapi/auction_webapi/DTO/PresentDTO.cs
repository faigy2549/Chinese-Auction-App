using auction_webapi.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace auction_webapi.DTO
{
    public class PresentDTO
    {
        [Key]
        [NotNull]

        public int PresentId { get; set; }
        public string Name { get; set; }

        public int? DonarId { get; set; }

        [NotNull]
        public EnumPresentCategory Category { get; set; }

        public double Price { get; set; }

        public string Image_Url { get; set; }
        public int Pcount { get; set; }

        public string? Winner { get; set; } = null;
    }
}
