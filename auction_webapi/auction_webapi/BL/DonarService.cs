using auction_webapi.DAL;
using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mail;
using static System.Net.Mail.MailAddress;

namespace auction_webapi.BL
{
    public class DonarService:IDonarService
    {
        private IDonarDAL _donarDAL;
        public DonarService(IDonarDAL donarDAL)
        {
                this._donarDAL= donarDAL ?? throw new ArgumentNullException(nameof(donarDAL));       
        }
        public Task<List<Donar>> GetAsync()
        {
            return _donarDAL.GetAsync();
        }
        public Task<Donar> GetByIdAsync(int id)
        {
            return _donarDAL.GetByIdAsync(id);
        }
        public Task<List<Donar>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email)
        {
            return _donarDAL.GetByFilterAsync(name, lname,presentid,email);
        }

        public Task<string> DeleteAsync(int id)
        {
            return _donarDAL.DeleteByIdAsync(id);
        }
        public async Task<Donar> PostAsync(Donar d)
        {

            if (!ValidateEmail(d.Email))
            {
                throw new Exception("invalid email");
            }
            Donar newd = await _donarDAL.PostAsync(d);


            return newd;

        }
    

        public async Task<string> PutAsync(Donar d)
        {

            if (!ValidateEmail(d.Email))
            {
                throw new Exception("invalid email");
            }
            return await _donarDAL.PutAsync(d);

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
