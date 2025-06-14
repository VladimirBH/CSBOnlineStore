using CSBOnlineStore.DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace CSBOnlineStore.DataBase
{
    public class CSBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategorySpetification> CategorySpetifications { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Spetification> Spetifications { get; set; }
        public DbSet<UnitProduct> UnitProducts { get; set; }
        public DbSet<SpetificationProduct> SpetificationProducts { get; set; }

        public CSBContext(DbContextOptions<CSBContext> contextOptions) : base(contextOptions) 
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
            Database.EnsureDeleted(); 
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresEnum("status", Enum.GetNames<Status>());
            modelBuilder.HasPostgresEnum("payment_type", Enum.GetNames<PaymentType>());
        }
    }
}
