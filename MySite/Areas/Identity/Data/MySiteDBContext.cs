using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySite.Areas.Identity.Data;
using MySite.Models;
using System.Reflection.Emit;

namespace MySite.Data;

public class MySiteDBContext : IdentityDbContext<User>
{
    public MySiteDBContext(DbContextOptions<MySiteDBContext> options)
        : base(options)
    {
    }
    public DbSet<Product> Product { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

    }
}
