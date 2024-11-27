using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.BL
{
    public interface IOrderItemService
    {
        public Task<List<OrderItem>> GetAsync();
        public Task<OrderItem> GetByIdAsync(int id);
        public Task<ActionResult<List<OrderItem>>> GetCartAsync(int userid);
        public Task<OrderItem> PostAsync(OrderItem o);
        public Task<string> PutAsync(OrderItem o);
        public Task<string> DeleteByIdAsync(int id);
    }
}
