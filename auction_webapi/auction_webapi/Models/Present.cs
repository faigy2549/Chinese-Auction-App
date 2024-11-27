using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace auction_webapi.Models
{
    public class Present
 
    {
        [Key]
        [NotNull]
   
        public int PresentId { get; set; }
        public string Name { get; set; }

        public int? DonarId { get; set; }
        public Donar? Donar { get; set; }

        [NotNull]
        public EnumPresentCategory Category { get; set; }

        public double Price { get; set; }

        public string Image_Url { get; set; }
        public int Pcount { get; set; }
     
        public string? Winner { get; set; } = null;
    }
}
