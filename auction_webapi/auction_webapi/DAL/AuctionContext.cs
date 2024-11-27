using auction_webapi.Models;
using Microsoft.EntityFrameworkCore;
using auction_webapi.Models;

namespace auction_webapi.DAL
{
    public class AuctionContext:DbContext
    {
        public AuctionContext(DbContextOptions<AuctionContext> options) : base(options)
        {
        }
        public DbSet<Donar> Donars { get; set; }
        public DbSet<Present> Presents { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Winner> Winners { get; set; }
        public DbSet<Raffle> Raffles { get; set; }


    }

}