using BaseStartupProject.Infrastructure.Repositories.DataTableObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace BaseStartupProject.Infrastructure.Repositories.EntityFramework
{
    public class BaseStartupDemo : DbContext
    {
        const string connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=BaseStartupDemo;Integrated Security=SSPI;";
        public BaseStartupDemo() : base()
        {
            
        }

        public BaseStartupDemo(DbContextOptions<BaseStartupDemo> options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<DtoUser> Users { get; set; }
        public DbSet<DtoConfiguration> Configurations { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) {             
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
