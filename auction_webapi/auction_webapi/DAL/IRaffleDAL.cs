using auction_webapi.Models;

namespace auction_webapi.DAL
{
    public interface IRaffleDAL
    {
        public Task<Winner> Raffle(Present p);
        public  Task<string> Results();
        public Task<string> Income();
    }
}
