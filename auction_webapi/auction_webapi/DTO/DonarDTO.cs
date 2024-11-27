using auction_webapi.Models;

namespace auction_webapi.DTO
{
    public class DonarDTO
    {

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public ICollection<PresentDTO> Presents { get; set; }
    }
}
