﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySite.Models;

namespace MySite.Data
{
    public class MySiteContext : DbContext
    {
        public MySiteContext (DbContextOptions<MySiteContext> options)
            : base(options)
        {
        }

        public DbSet<MySite.Models.Product> Product { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<MySite.Models.Wishlist> Wishlist { get; set; } = default!;
    }
}
