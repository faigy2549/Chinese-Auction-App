using auction_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Linq;
using System.Security.Claims;
using auction_webapi.DTO;
namespace auction_webapi.DAL
{
    public class OrderDAL : IOrderDAL
    {
        private AuctionContext _auctionContext;
        public OrderDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<Order>> GetAsync()
        {
            try {
                return await _auctionContext.Orders
                         .ToListAsync();

            }
            catch(Exception ex)
            {
                throw new Exception("can't get");
            }
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _auctionContext.Orders
                           .FirstOrDefaultAsync(d => d.OrderId == id);
        }

        public async Task<List<Order>> GetByUserAsync(int UserId)
        {

            throw new NotImplementedException();

        }

        public Task<List<Order>> GetByUserAsync(string UserName)
        {
            throw new NotImplementedException();
        }

        public async Task<Order> PostAsync(Order o)
        {
            try
            {
                List<OrderItem> orderitems=await _auctionContext.OrderItem.Where(a=>a.UserId==o.UserId&&a.Status==false)
                        .ToListAsync();

                foreach (OrderItem item in orderitems)
                {

                    Present p = await _auctionContext.Presents.FindAsync(item.PresentId);
                    p.Pcount+=item.Quantity;
                    await _auctionContext.SaveChangesAsync();
                    item.Status = true;
                    await _auctionContext.SaveChangesAsync();
                }
            
                _auctionContext.Orders.Add(o);
                await _auctionContext.SaveChangesAsync();
                foreach (OrderItem item in orderitems)
                {
                    item.OrderId = o.OrderId;
                    await _auctionContext.SaveChangesAsync();
                }

                    return o;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the Order entity.", ex);
            }
        }

    
    }
}
