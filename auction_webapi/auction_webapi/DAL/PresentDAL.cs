
using auction_webapi.BL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

namespace auction_webapi.DAL
{
    public class PresentDAL : IPresentDAL
    {
        private AuctionContext _auctionContext;
        public PresentDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<Present>> GetAsync(string orderBy)
        {
            try
            {
                IQueryable<Present> query = _auctionContext.Presents;

                if (orderBy == "mostExpensive")
                {
                    query = query.OrderByDescending(p => p.Price);
                }
                else if (orderBy == "mostBought")
                {
                    query = query.OrderByDescending(p => p.Pcount);
                }
                else
                {
                    query = query.OrderBy(p => p.Name);
                }

                return await query.ToListAsync();
            }

            catch (Exception ex)
            {
                throw new Exception("can't get");
            }
        }

        public async Task<Present> GetByIdAsync(int id)
        {
            return await _auctionContext.Presents.FindAsync(id);

        }
        public async Task<List<Present>> GetByFilterAsync(int? amount,string? donarname,string? name)
        {
            var query = _auctionContext.Presents.Where(present =>
            (name == null ? (true) : (present.Name.Contains(name)))
            && ((amount == null) ? (true) : (present.Pcount == amount))
            && ((donarname == null) ? (true) : (present.Donar.FirstName == donarname)));
            List<Present> presents = await query.ToListAsync();
            return presents;
        }

        public async Task<List<Present>> GetPresentByCategory(int category)
        {
               return await _auctionContext.Presents.Where(p => (int)p.Category == category).ToListAsync();

          
        }

        public async Task<string> DeleteByIdAsync(int id)
        {
            Present p = await _auctionContext.Presents.FindAsync(id);
            _auctionContext.Presents.Remove(p);
            await _auctionContext.SaveChangesAsync();
            return $" Present {p.Name} was Deleted";
        }


        public async Task<string> PostAsync(Present p)
        {

                await _auctionContext.Presents.AddAsync(p);
                await _auctionContext.SaveChangesAsync();
                return $"Added present {p.Name}.";
       
}

        public async Task<string> PutAsync(Present p)
        {
            _auctionContext.Presents.Update(p);
            await _auctionContext.SaveChangesAsync();
            return $"present {p.Name} updated";
        }
    }
}
