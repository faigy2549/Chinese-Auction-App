using auction_webapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace auction_webapi.DAL
{
    public class UserDAL : IUserDAL
    {
        private AuctionContext _auctionContext;
        public UserDAL(AuctionContext auctionContext)
        {
            this._auctionContext = auctionContext ?? throw new ArgumentNullException(nameof(auctionContext));
        }
        public async Task<List<User>> GetAsync()
        {
            try { 
            return await _auctionContext.Users
                    .Include(d => d.Orders)
                     .ToListAsync();
      
            }
            catch(Exception ex)
            {
                throw new Exception("can't get users");
            }
        }
        public async Task<User> GetByIdAsync(int id)
        {
            return await _auctionContext.Users
                .Include(d => d.Orders) 
                .FirstOrDefaultAsync(d => d.UserId == id);
        }
        public async Task<User> PostAsync(User u)
        {
            try
            {
                await _auctionContext.Users.AddAsync(u);
                await _auctionContext.SaveChangesAsync();
                return u;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving the User entity.", ex);
            }
        }
        public async Task<string> PutAsync(User u)
        {
            _auctionContext.Users.Update(u);
            await _auctionContext.SaveChangesAsync();
            return $"user {u.FirstName} updated";
        }
        public async Task<string> DeleteByIdAsync(int id)
        {
            User u = await _auctionContext.Users.FindAsync(id);
             _auctionContext.Users.Remove(u);
            await _auctionContext.SaveChangesAsync();
            return $" User {u.FirstName} was Deleted";

        }


        public async Task<User> GetUserByUsernameAndPassword(string email, string password)
        {

                User u = await _auctionContext.Users
                    .Where(v => v.Email == email && v.Password == password)
                    .FirstOrDefaultAsync();
                return u;
            

        }




    }
}
