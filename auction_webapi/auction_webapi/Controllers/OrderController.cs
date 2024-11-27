using auction_webapi.BL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.Controllers
{
   
    [ApiController]
    [Route("api/[Controller]")]
    public class OrderController : ControllerBase
    {
        private IOrderService _orderService;
        private IMapper _imapper;
        public OrderController(IOrderService OrderService, IMapper imapper)
        {
            this._orderService = OrderService ?? throw new ArgumentNullException(nameof(OrderService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }

        [HttpGet("GetOrders")]
        public async Task<ActionResult<List<OrderDTO>>> GetOrders()
        {
            List<Order> orders = await _orderService.GetAsync();
            List<OrderDTO> orderDTOs = _imapper.Map<List<Order>, List<OrderDTO>>(orders);
            return orderDTOs;
        }
        [HttpPost("postOrder")]
        public async Task<ActionResult<Order>> PostOrder([FromBody] OrderDTO order)
        {
            try
            {
                Order o = _imapper.Map<OrderDTO, Order>(order);
                var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "userId");
                if (int.TryParse(userIdClaim?.Value, out int userId))
                {
                    o.UserId = userId;
                    return await _orderService.PostAsync(o);
                }
                else
                {
                    return BadRequest("Invalid UserId claim value.");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getOrderByID/{id}")]
        public async Task<ActionResult<OrderDTO>> GetOrderById(int id)
        {
            Order orders = await _orderService.GetByIdAsync(id);
            OrderDTO orderDTOs = _imapper.Map<Order, OrderDTO>(orders);
            //IEnumerable<OrderItemDTO> oiDTOs = _imapper.Map<ICollection<OrderItem>, IEnumerable<OrderItemDTO>>(orders.OrderItems);
            //orderDTOs.OrderItems = oiDTOs.ToList();
            return orderDTOs;
        }
        [HttpGet("getOrderByUserName/{UserName}")]
        public async Task<ActionResult<List<OrderDTO>>> GetByUserId(string userName)
        {
            List<Order> orders = await _orderService.GetByUserAsync(userName);
            List<OrderDTO> orderDTOs = _imapper.Map<List<Order>, List<OrderDTO>>(orders);

            //foreach (Order order in orders)
            //{
            //    IEnumerable<OrderItemDTO> oiDTOs = _imapper.Map<ICollection<OrderItem>, IEnumerable<OrderItemDTO>>(order.OrderItems);
            //    orderDTOs.First(o => o.OrderId == order.OrderId).OrderItems = oiDTOs.ToList();
            //}

            return orderDTOs;
        }
    }
}
