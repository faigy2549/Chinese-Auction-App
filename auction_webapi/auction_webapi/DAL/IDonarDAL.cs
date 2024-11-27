using auction_webapi.Models;

namespace auction_webapi.DAL
{
    public interface IDonarDAL
    {
        public Task<List<Donar>> GetAsync();
        public  Task<Donar> GetByIdAsync(int id);
        public Task<List<Donar>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email);
        public  Task<Donar> PostAsync(Donar d);
        public  Task<string> PutAsync(Donar d);
        public Task<string> DeleteByIdAsync(int id);
    }
}
