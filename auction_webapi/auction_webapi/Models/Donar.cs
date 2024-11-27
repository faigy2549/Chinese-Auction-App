using auction_webapi.DTO;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace auction_webapi.Models
{

    public class Donar
    {
      
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        
        public ICollection<Present> Presents { get; set; }
    }
}
