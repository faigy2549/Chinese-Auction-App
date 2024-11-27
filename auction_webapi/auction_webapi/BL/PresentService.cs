using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using System.Xml.Linq;

namespace auction_webapi.BL
{
    public class PresentService:IPresentService
    {
        private IPresentDAL _presentDAL;
        public PresentService(IPresentDAL presentDAL)
        {
            this._presentDAL = presentDAL ?? throw new ArgumentNullException(nameof(presentDAL));
        }
        public async Task<List<Present>> GetAsync(string orderBy)
        {
            var presents = await _presentDAL.GetAsync(orderBy);

            return presents;
        }
        public Task<Present> GetByIdAsync(int id)
        {
            return _presentDAL.GetByIdAsync(id);
        }
        public Task<List<Present>> GetByFilterAsync(int? amount, string? donarname, string? name)
        {
            return _presentDAL.GetByFilterAsync( amount, donarname,  name);
        }
        public async Task<List<Present>> GetPresentByCategory(int category)
        {
            return await  _presentDAL.GetPresentByCategory(category);

        }
        public Task<string> DeleteByIdAsync(int id)
        {
            return _presentDAL.DeleteByIdAsync(id);
        }
        public async Task<string> PostAsync(Present p)
        {
            
          
            return await _presentDAL.PostAsync(p);
        
                throw new Exception("no donarId");
     
        }

        public async Task<string> PutAsync(Present p)
        {

            return await _presentDAL.PutAsync(p);

        }
        public bool ValidateEmail(string email)
        {
            try
            {
                MailAddress mailAddress = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
