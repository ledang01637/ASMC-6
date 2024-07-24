using Microsoft.EntityFrameworkCore;
using ASMC6.Shared;

namespace ASMC6.Server.Data
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options ) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>().Property(c => c.TotalAmount).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>().Property(d => d.Price).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(f => f.Price).HasColumnType("decimal(18,2)");

            modelBuilder.Entity<User>().HasKey(a => a.UserId);
            modelBuilder.Entity<Restaurant>().HasKey(b => b.RestaurantId);
            modelBuilder.Entity<Menu>().HasKey(c => c.MenuId);
            modelBuilder.Entity<Order>().HasKey(d => d.OrderId);
            modelBuilder.Entity<OrderItem>().HasKey(e => e.OrderItemId);
            modelBuilder.Entity<Category>().HasKey(k => k.CategoryId);
            modelBuilder.Entity<Product>().HasKey(l => l.ProductId);
            modelBuilder.Entity<Role>().HasKey(l => l.RoleId);


            modelBuilder.Entity<Menu>()
                .HasOne(b => b.Restaurant)
                .WithMany(b => b.Menus)
                .HasForeignKey(b => b.RestaurantId);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Menu)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.MenuId);

            modelBuilder.Entity<Product>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(c => c.CategoryId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(e => e.Product)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(e => e.Order)
                .WithMany(e => e.OrderItems)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<Order>()
                .HasOne(f => f.User)
                .WithMany(f => f.Orders)
                .HasForeignKey(f => f.UserId);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(u => u.Users)
                .HasForeignKey(u => u.RoleId);

        }
        public DbSet<Product> Product {  get; set; }
        public DbSet<Menu> Menu {  get; set; }
        public DbSet<Role> Role {  get; set; }
        public DbSet<User> User {  get; set; }
        public DbSet<Restaurant> Restaurant {  get; set; }
        public DbSet<Order> Order {  get; set; }
        public DbSet<OrderItem> OrderItem {  get; set; }
        public DbSet<Category> Category {  get; set; }
    }
}
