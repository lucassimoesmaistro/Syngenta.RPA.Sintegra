using Microsoft.EntityFrameworkCore;
using Syngenta.Common.Data;
using Syngenta.Sintegra.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syngenta.Sintegra.Repository
{
    public class CustomerContext : DbContext, IUnitOfWork
    {
        public CustomerContext(DbContextOptions<CustomerContext> options) : base(options) { }

        public DbSet<Request> RequestVerification { get; set; }
        public DbSet<RequestItem> RequestItems { get; set; }
        public DbSet<ChangeLog> ChangeLogs { get; set; }
        public DbSet<Customer> Customers { get; set; }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("Timestamp") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("Timestamp").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("Timestamp").IsModified = false;
                }
            }

            return await base.SaveChangesAsync() > 0;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(
                e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(100)");

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CustomerContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            base.OnModelCreating(modelBuilder);
        }
    }
    
}
