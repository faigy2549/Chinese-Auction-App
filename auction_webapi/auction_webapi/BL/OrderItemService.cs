using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.BL
{
    public class OrderItemService : IOrderItemService
    {
        private IOrderItemDAL _orderItemDAL;
        public OrderItemService(IOrderItemDAL orderItemDAL)
        {
            _orderItemDAL = orderItemDAL ?? throw new ArgumentNullException(nameof(orderItemDAL));
        }
        public  Task<List<OrderItem>> GetAsync()
        {
          return _orderItemDAL.GetAsync();
        }

        public Task<OrderItem> GetByIdAsync(int id)
        {
            return _orderItemDAL.GetByIdAsync(id);


        }
        public Task<ActionResult<List<OrderItem>>> GetCartAsync(int userid)
        {
            return _orderItemDAL.GetCartAsync(userid);
        }

        public  Task<OrderItem> PostAsync(OrderItem o)
        {
            return _orderItemDAL.PostAsync(o);

        }

        public Task<string> PutAsync(OrderItem o)
        {
            return _orderItemDAL.PutAsync(o);
        }

        public Task<string> DeleteByIdAsync(int id)
        {
            return _orderItemDAL.DeleteByIdAsync(id);
        }
    }
}
