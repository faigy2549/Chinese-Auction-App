using auction_webapi.Models;

namespace auction_webapi.BL
{
    public interface IDonarService
    {
        public Task<List<Donar>> GetAsync();
        public Task<Donar> GetByIdAsync(int id);
        public Task<List<Donar>> GetByFilterAsync(string? name, string? lname, int? presentid, string? email);
        public Task<Donar> PostAsync(Donar d);
        public Task<string> PutAsync(Donar donar);

        public Task<string> DeleteAsync(int id);

    }
}
