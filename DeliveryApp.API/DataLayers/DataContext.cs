using DeliveryApp.API.DataLayers.Entities;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.DataLayers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DeliveryAppSchema");
            modelBuilder.Entity<Auth>().ToTable("Auth").HasKey(k => k.AuthId);

            modelBuilder.Entity<DeliveryAgent>().ToTable("DeliveryAgent").HasKey(k => k.DeliveryAgentId);

            modelBuilder.Entity<MenuItem>().ToTable("MenuItem").HasKey(k => k.MenuItemId);
            modelBuilder.Entity<MenuItem>().HasOne(o => o.Restaurant).WithMany(m => m.MenuItems).HasForeignKey(k => k.RestaurantId);
            modelBuilder.Entity<MenuItem>().HasMany(m => m.OrderItems).WithOne(o => o.MenuItem).HasForeignKey(k => k.OrderItemId);

            modelBuilder.Entity<Order>().ToTable("Order").HasKey(k => k.OrderId);
            modelBuilder.Entity<Order>().HasOne(o => o.Restaurant).WithMany(m => m.Orders).HasForeignKey(k => k.RestaurantId);
            modelBuilder.Entity<Order>().HasMany(m => m.OrderItems).WithOne(m => m.Order).HasForeignKey(k => k.OrderItemId);

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem").HasKey(k => k.OrderItemId);
            modelBuilder.Entity<OrderItem>().HasOne(o => o.MenuItem).WithMany(m => m.OrderItems).HasForeignKey(k => k.MenuItemId);

            modelBuilder.Entity<Payment>().ToTable("Payment").HasKey(k => k.PaymentId);

            modelBuilder.Entity<Restaurant>().ToTable("Restaurant").HasKey(k => k.RestaurantId);
            modelBuilder.Entity<Restaurant>().HasMany(m => m.Orders).WithOne(o => o.Restaurant).HasForeignKey(k => k.OrderId);
            modelBuilder.Entity<Restaurant>().HasMany(m => m.MenuItems).WithOne(o => o.Restaurant).HasForeignKey(k => k.MenuItemId);

            modelBuilder.Entity<User>().ToTable("User").HasKey(k => k.UserId);
        }
    }
}
