using Application.Interfaces.Context;
using Domain.Entities.Categories;
using Domain.Entities.Common;
using Domain.Entities.Products;
using Domain.Entities.User;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Context
{
    public class DataBaseContext : DbContext, IDataBaseContext
    {
        public DataBaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductFeatures> ProductFeatures { get; set; }
        public DbSet<ProductImages> Images { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            FilterQuery(modelBuilder);
        }
        private void FilterQuery(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<Product>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductFeatures>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<ProductImages>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<User>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<UserRoleType>().HasQueryFilter(p => !p.IsRemoved);
            modelBuilder.Entity<RoleType>().HasQueryFilter(p => !p.IsRemoved);
        }

    }
}
