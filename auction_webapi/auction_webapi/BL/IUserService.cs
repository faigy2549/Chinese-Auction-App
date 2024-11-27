using auction_webapi.Models;

namespace auction_webapi.BL
{
    public interface IUserService
    {
        public Task<List<User>> GetAsync();
        public Task<User> GetByIdAsync(int id);
        public Task<User> PostAsync(User d);
        public Task<string> PutAsync(User d);
        public Task<string> DeleteByIdAsync(int id);
        public Task<User> GetUserByUsernameAndPassword(string email, string password);

    }
}