using Microsoft.EntityFrameworkCore;
using PizzaSales.Models;

namespace PizzaSalesAPI.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Pizza> Pizzas { get; set; }
        public DbSet<PizzaType> PizzaTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne(od => od.Order)
                .HasForeignKey(od => od.OrderId);

            modelBuilder.Entity<OrderItem>()
                .Property(od => od.OrderId)
                .IsRequired();

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Id)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<OrderItem>()
                .HasKey(o => new { o.OrderId, o.PizzaId });

            modelBuilder.Entity<Pizza>()
                .HasKey(p => new { p.PizzaTypeId });

            modelBuilder.Entity<Pizza>()
                .HasKey(p => p.Id);

            modelBuilder.Entity<Pizza>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<PizzaType>()
                .HasKey(pt => pt.Id);
        }

    }
}
