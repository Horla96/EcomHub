using EcomHub.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EcomHub.Infrastructure; 

public class AppDbContext : IdentityDbContext<User, Role, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    public DbSet<CartItems> CartItems { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Otp> Otps { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();

        builder.Entity<Role>().HasData(
        new Role
        {
            Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
            Name = "Admin",
            NormalizedName = "ADMIN",
            Description = "Admin role",
            CreatedBy = "System",
            CreatedAt = DateTime.Now
        },
        new Role
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Name = "Customer",
            NormalizedName = "CUSTOMER",
            Description = "Customer role",
            CreatedBy = "System",
            CreatedAt = DateTime.Now
        });
    }
}
