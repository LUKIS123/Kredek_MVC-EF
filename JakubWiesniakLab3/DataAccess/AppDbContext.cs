using JakubWiesniakLab3.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace JakubWiesniakLab3.DataAccess
{
    public class AppDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<User> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasKey(product => product.Id);

            modelBuilder.Entity<Product>()
                .Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(64);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(5, 2);

            modelBuilder.Entity<Product>()
                .HasMany(product => product.OrderItems)
                .WithOne(orderItem => orderItem.Product)
                .HasForeignKey(orderItem => orderItem.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<User>()
                .HasKey(user => user.Id);

            modelBuilder.Entity<User>()
                .HasMany(user => user.Orders)
                .WithOne(order => order.User)
                .HasForeignKey(order => order.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasKey(order => order.Id);

            modelBuilder.Entity<Order>()
                .Property(o => o.Date)
                .HasDefaultValueSql("GETDATE()");

            modelBuilder.Entity<Order>()
                .HasMany(order => order.OrderItems)
                .WithOne(orderItem => orderItem.Order)
                .HasForeignKey(orderItem => orderItem.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<OrderItem>()
                .HasKey(orderItem => orderItem.Id);

            modelBuilder.Entity<OrderStatus>()
                .HasKey(orderStatus => orderStatus.Id);

            // modelBuilder.Entity<Order>()
            //     .HasOne(order => order.User)
            //     .WithMany(user => user.Orders)
            //     .HasForeignKey(order => order.UserId);

            // modelBuilder.Entity<OrderItem>()
            //     .HasOne(orderItem => orderItem.Product)
            //     .WithMany(product => product.OrderItems)
            //     .HasForeignKey(orderItem => orderItem.ProductId);
            //
            // modelBuilder.Entity<OrderItem>()
            //     .HasOne(orderItem => orderItem.Order)
            //     .WithMany(order => order.OrderItems)
            //     .HasForeignKey(orderItem => orderItem.OrderId);


            base.OnModelCreating(modelBuilder);
        }
    }
}