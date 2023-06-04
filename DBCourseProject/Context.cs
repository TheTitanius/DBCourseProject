using DBCourseProject.Entities;
using Microsoft.EntityFrameworkCore;

namespace DBCourseProject
{
    public class Context : DbContext
    {
        public Context()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(@"host=localhost;port=5432;database=dealerSystem;username=postgres;password=16368");
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<TVType> TVTypes { get; set; }
        public DbSet<TV> TVs { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<Bill> Bills { get; set; }
        public DbSet<GoodDelivery> GoodsDelivery { get; set; }
    }
}
