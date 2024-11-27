using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.DAL
{
    public interface IOrderItemDAL
    {
        public Task<List<OrderItem>> GetAsync();
        public Task<OrderItem> GetByIdAsync(int id);
        public Task<ActionResult<List<OrderItem>>> GetCartAsync(int userid);
        public Task<OrderItem> PostAsync(OrderItem d);
        public Task<string> PutAsync(OrderItem d);
        public Task<string> DeleteByIdAsync(int id);
    }
}
