using DeliveryApp.API.DataLayers.Entities;
using DeliveryAppBackend.DataLayers.Entities;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.API.DataLayers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<Auth> Auths { get; set; }
        public DbSet<DeliveryAgent> DeliveryAgents { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Restaurant> Restaurants { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("DeliveryAppSchema");
            modelBuilder.Entity<Auth>().ToTable("Auth").HasKey(k => k.AuthId);

            modelBuilder.Entity<DeliveryAgent>().ToTable("DeliveryAgent").HasKey(k => k.DeliveryAgentId);

            modelBuilder.Entity<MenuItem>().ToTable("MenuItem").HasKey(k => k.MenuItemId);
            modelBuilder.Entity<MenuItem>().Property(m => m.MenuItemId).UseIdentityColumn();
            modelBuilder.Entity<MenuItem>().Property(m => m.MenuItemId).ValueGeneratedOnAdd();

            modelBuilder.Entity<MenuItem>().HasOne(o => o.Restaurant).WithMany(m => m.MenuItems).HasForeignKey(k => k.RestaurantId);
            modelBuilder.Entity<MenuItem>().HasMany(m => m.OrderItems).WithOne(o => o.MenuItem).HasForeignKey(k => k.OrderItemId);
            modelBuilder.Entity<MenuItem>().HasOne(o => o.Category).WithMany(m => m.MenuItems).HasForeignKey(k => k.MenuItemId);
            modelBuilder.Entity<MenuItem>().Property(m => m.Price).HasColumnType("decimal(18,2)"); // Specify store type for Price

            modelBuilder.Entity<Category>().ToTable("Category").HasKey(k => k.CategoryId);
            modelBuilder.Entity<Category>().HasMany(m => m.MenuItems).WithOne(o => o.Category).HasForeignKey(k => k.CategoryId);

            modelBuilder.Entity<Order>().ToTable("Order").HasKey(k => k.OrderId);
            modelBuilder.Entity<Order>().HasOne(o => o.Restaurant).WithMany(m => m.Orders).HasForeignKey(k => k.RestaurantId);
            modelBuilder.Entity<Order>().HasMany(m => m.OrderItems).WithOne(m => m.Order).HasForeignKey(k => k.OrderItemId);
            modelBuilder.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)"); // Specify store type for TotalAmount

            modelBuilder.Entity<OrderItem>().ToTable("OrderItem").HasKey(k => k.OrderItemId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.Order)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(k => k.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(o => o.MenuItem)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(k => k.MenuItemId);


            modelBuilder.Entity<OrderItem>().Property(oi => oi.TotalPrice).HasColumnType("decimal(18,2)"); // Specify store type for TotalPrice

            modelBuilder.Entity<Payment>().ToTable("Payment").HasKey(k => k.PaymentId);
            modelBuilder.Entity<Payment>().Property(p => p.Amount).HasColumnType("decimal(18,2)"); // Specify store type for Amount

            modelBuilder.Entity<Restaurant>().ToTable("Restaurant").HasKey(k => k.RestaurantId);
            modelBuilder.Entity<Restaurant>().HasMany(m => m.Orders).WithOne(o => o.Restaurant).HasForeignKey(k => k.OrderId);
            modelBuilder.Entity<Restaurant>().HasMany(m => m.MenuItems).WithOne(o => o.Restaurant).HasForeignKey(k => k.RestaurantId); // Changed from MenuItemId to RestaurantId

            modelBuilder.Entity<User>().ToTable("User").HasKey(k => k.UserId);


            /*            modelBuilder.Entity<Location>().ToTable("Location").HasNoKey();*/
        }
    }
}
