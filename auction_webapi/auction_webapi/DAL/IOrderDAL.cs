using auction_webapi.Models;

namespace auction_webapi.DAL
{
    public interface IOrderDAL
    {
        public Task<List<Order>> GetAsync();
        public Task<Order> GetByIdAsync(int id);
        public Task<Order> PostAsync(Order o);
        public Task<List<Order>> GetByUserAsync(string UserName);
    }
}
