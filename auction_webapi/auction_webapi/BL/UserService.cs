using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using SendGrid.Helpers.Errors.Model;

namespace auction_webapi.BL
{
    public class UserService : IUserService
    {
        private readonly IUserDAL _userDAL;

        public UserService(IUserDAL userDAL)
        {
            this._userDAL = userDAL ?? throw new ArgumentNullException(nameof(userDAL));
        }
        public Task<List<User>> GetAsync()
        {
            return _userDAL.GetAsync();
        }
 

        public Task<User> GetByIdAsync(int id)
        {
            return _userDAL.GetByIdAsync(id);
        }

        public Task<User> PostAsync(User d)
        {
            return _userDAL.PostAsync(d);
        }

        public Task<string> PutAsync(User d)
        {
            return _userDAL.PutAsync(d);
        }

        public Task<string> DeleteByIdAsync(int id)
        {
            return _userDAL.DeleteByIdAsync(id);
        }
        public Task<User> GetUserByUsernameAndPassword(string email, string password)

        {
     
                return _userDAL.GetUserByUsernameAndPassword(email, password);
           

        }
    }
}
