using auction_webapi.BL;
using auction_webapi.DTO;
using auction_webapi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace auction_webapi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemService _OrderItemService;
        private IMapper _imapper;
        public OrderItemController(IOrderItemService orderItemService, IMapper imapper)
        {
            this._OrderItemService = orderItemService ?? throw new ArgumentNullException(nameof(orderItemService));
            this._imapper = imapper ?? throw new ArgumentNullException(nameof(imapper));
        }
    
        [HttpGet("GetOrderItems")]
      
        public async Task<ActionResult<List<OrderItem>>> GetOrderItems()
        {
            return await _OrderItemService.GetAsync();
        }
        [HttpGet("GetCart")]
        public async Task<ActionResult<List<OrderItem>>> GetCart()
        {
            var user = User.Claims.FirstOrDefault(c => c.Type == "userId");
            int.TryParse(user?.Value, out int userId);
            return await _OrderItemService.GetCartAsync(userId);
        }

        [HttpGet("GetItem/{id}")]
        public async Task<ActionResult<OrderItem>> GetOrderItemById(int id)
        {
            return await _OrderItemService.GetByIdAsync(id);
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<ActionResult<string>> DeleteOrderItem(int id)
        {
            return await _OrderItemService.DeleteByIdAsync(id);
        }
        [HttpPost("createOrderItem")]
        public async Task<ActionResult<OrderItem>> PostOrderItem([FromBody] OrderItemDTO oi)
        {


            try {
                OrderItem ordi = _imapper.Map<OrderItemDTO, OrderItem>(oi);
                var user = User.Claims.FirstOrDefault(c => c.Type == "userId");
                int.TryParse(user?.Value, out int userId);
                ordi.UserId = userId;

                return await _OrderItemService.PostAsync(ordi);
            } 

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        [HttpPut("updateItem")]
        public async Task<ActionResult<string>> UpdatePresent([FromBody] OrderItemDTO oi)
        {
            try
            {
                OrderItem ordi = _imapper.Map<OrderItemDTO, OrderItem>(oi);
                return await _OrderItemService.PutAsync(ordi);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
