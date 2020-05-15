using IntimateDesires.Infrastructure.Repositories.DataTableObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntimateDesires.Infrastructure.Repositories.EntityFramework
{
    public class IntimateDesiresContext : DbContext
    {
        const string connectionString = "Server=93.189.89.134;Database=IntimateDesiresDev;User Id=sa;Password=!JMG190390JMG!;";
        public IntimateDesiresContext() : base()
        {
            
        }

        public IntimateDesiresContext(DbContextOptions<IntimateDesiresContext> options) : base(options) {
            this.ChangeTracker.LazyLoadingEnabled = false;
        }

        public DbSet<DtoUser> Users { get; set; }

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
