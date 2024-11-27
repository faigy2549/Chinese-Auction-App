using auction_webapi.Models;

namespace auction_webapi.BL
{
    public interface IRaffleService
    {
        public Task<Winner> Raffle(Present p);
        public Task<string> Results();
        public  Task<string> Income();
    }
}