using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySite.Areas.Identity.Data;
using MySite.Models;

namespace MySite.Data
{
    public class MySiteContext : IdentityDbContext<IdentityUser>
    {
        public MySiteContext(DbContextOptions<MySiteContext> options)
            : base(options)
        {
        }

        public DbSet<Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<Wishlist> Wishlist { get; set; } = default!;
        public DbSet<Cart> Carts { get; set; } = default!;
        public DbSet<CartItem> CartItems { get; set; } = default!;
        public DbSet<Brand> Brand { get; set; } = default!;
        public DbSet<AnimalType> AnimalType { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Настройки для Product
            modelBuilder.Entity<User>().ToTable("AspNetUsers");

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductWeight)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany()
                .HasForeignKey(p => p.CategoryId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Brand)
                .WithMany()
                .HasForeignKey(p => p.BrandId)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .HasOne(p => p.AnimalType)
                .WithMany()
                .HasForeignKey(p => p.AnimalTypeId)
                .IsRequired();


            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.User)
                .WithMany()
                .HasForeignKey(w => w.UserId)
                .IsRequired();
            modelBuilder.Entity<Wishlist>()
                .HasOne(w => w.Product)
                .WithMany()
                .HasForeignKey(w => w.ProductId);


        }
    }
}
