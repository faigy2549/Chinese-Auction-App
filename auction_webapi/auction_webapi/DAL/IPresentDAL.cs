using auction_webapi.Models;
using Microsoft.AspNetCore.Mvc;

namespace auction_webapi.DAL
{
    public interface IPresentDAL
    {
        public Task<List<Present>> GetAsync(string orderBy);
        public Task<Present> GetByIdAsync(int id);
        public Task<List<Present>> GetByFilterAsync(int? amount, string? donarname, string? name);
        public Task<List<Present>> GetPresentByCategory(int category);
        public Task<string> PostAsync(Present d);
        public Task<string> PutAsync(Present d);
        public Task<string> DeleteByIdAsync(int id);

    }
}