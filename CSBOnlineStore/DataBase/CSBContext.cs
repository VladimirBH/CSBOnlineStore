using CSBOnlineStore.DataBase.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CSBOnlineStore.DataBase
{
    public class CSBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<CartProduct> CartProducts { get; set; }
        public DbSet<Category> Categories { get; set; }
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
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasPostgresEnum<Status>();
            modelBuilder.HasPostgresEnum<PaymentType>();
            modelBuilder.HasPostgresEnum<DataTypeSpet>();

            modelBuilder.Entity<Product>()
                .HasMany(p => p.Attributes)
                .WithOne(pa => pa.Product)
                .HasForeignKey(pa => pa.ProductId);

            modelBuilder.Entity<SpetificationProduct>()
                .HasOne(pa => pa.Spetification)
                .WithMany()
                .HasForeignKey(pa => pa.SpetificationId);
        }
    }
}
