using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.DAL
{
    public class OrderItemDAL : IOrderItemDAL
    {
        private AuctionContext _auctionContext;
        public OrderItemDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<OrderItem>> GetAsync()
        {
            try
            {
                return await _auctionContext.OrderItem.ToListAsync();
            }
            catch 
            {
                throw new Exception("can't get");
            }
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _auctionContext.OrderItem.FindAsync(id);
        }
        public async Task<ActionResult<List<OrderItem>>> GetCartAsync(int userid)
        {
                 return await _auctionContext.OrderItem
                .Where(oi => oi.UserId == userid && oi.Status == false)
                .ToListAsync();
        }

        public async Task<OrderItem> PostAsync(OrderItem o)
        {
            OrderItem oo = await _auctionContext.OrderItem
                .Where(p => p.UserId == o.UserId && p.PresentId == o.PresentId&&p.Status==false)
                .FirstOrDefaultAsync();
            if (oo!=null)
            {
                oo.Quantity = oo.Quantity + 1;
                _auctionContext.OrderItem.Update(oo);
                await _auctionContext.SaveChangesAsync();
                return oo;
            }
            await _auctionContext.OrderItem.AddAsync(o);
            await _auctionContext.SaveChangesAsync();
            return o;

        }

        public async Task<string> PutAsync(OrderItem o)
        {
            _auctionContext.OrderItem.Update(o);
            await _auctionContext.SaveChangesAsync();
            return $"item {o.Id} updated";
        }
        public async Task<string> DeleteByIdAsync(int id)
        {
            OrderItem o = await _auctionContext.OrderItem.FindAsync(id);
            _auctionContext.OrderItem.Remove(o);
            await _auctionContext.SaveChangesAsync();
            return $" item {o.Id} was Deleted";
        }
    }
}
