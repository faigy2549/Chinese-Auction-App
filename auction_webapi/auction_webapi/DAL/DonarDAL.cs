using auction_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace auction_webapi.DAL
{
    public class DonarDAL : IDonarDAL
    {
        private AuctionContext _auctionContext;
        public DonarDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<Donar>> GetAsync()
        {
            try { 
            return await _auctionContext.Donars
                    .Include(d => d.Presents)
                     .ToListAsync();
        
 
      
            }
            catch(Exception ex)
            {
                throw new Exception("can't get");
            }
        }
        public async Task<Donar> GetByIdAsync(int id)
        {
            return await _auctionContext.Donars
                .Include(d => d.Presents) 
                .FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task<List<Donar>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            var query = _auctionContext.Donars.Where(donar =>
             (name == null && lname==null ? (true) : (donar.FirstName.Contains(name)&& donar.LastName.Contains(lname)))
             && ((email == null) ? (true) : (donar.Email == email))
             && ((presentid == null) ? (true) : (donar.Presents.Any(p => p.PresentId == presentid) ))
             );
            List<Donar> donars = await query.ToListAsync();
            return donars;
        }
        public async Task<Donar> PostAsync(Donar d)
        {
            try
            {
                await _auctionContext.Donars.AddAsync(d);
                await _auctionContext.SaveChangesAsync();
                return d;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the Donar entity.", ex);
            }
        }
        public async Task<string> PutAsync(Donar d)
        {
            _auctionContext.Donars.Update(d);
            await _auctionContext.SaveChangesAsync();
            return $"donar {d.FirstName} updated";
        }
        public async Task<string> DeleteByIdAsync(int id)
        {
            Donar d = await _auctionContext.Donars.FindAsync(id);
             _auctionContext.Donars.Remove(d);
            await _auctionContext.SaveChangesAsync();
            return $" Donar {d.FirstName} was Deleted";

        }






    }
}
