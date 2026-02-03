using InventoryManagementSystem.Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace InventoryManagementSystem.DataAccess.Context
{
    internal class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {


        }

        public DbSet<Product> Products { get; set; }
        public DbSet<LogTable> LogsTable { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .HasMany(d => d.Logs)
                .WithOne(l => l.Product)
                .HasForeignKey(l => l.ProductId);
        }
    }
}
