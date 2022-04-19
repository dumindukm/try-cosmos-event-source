using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustodianService.Models;
using Microsoft.EntityFrameworkCore;

namespace CustodianService.Infrastructure
{
    public class CustodianContext :DbContext
    {
        //public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPortfolio> CustomerAssets { get; set; }
        public DbSet<CustomerSecurityTransaction> CustomerSecurityTransactions { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            "https://localhost:443",
            "",
            databaseName: "CustodianDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("CustomerPortfolio");

            //modelBuilder.Entity<Customer>().ToContainer("Customers").HasPartitionKey(x=> x.CdsIdentifier);
            modelBuilder.Entity<CustomerPortfolio>().ToContainer("CustomerPortfolio").HasPartitionKey(x=> x.CdsIdentifier);
            modelBuilder.Entity<CustomerSecurityTransaction>().ToContainer("CustomerSecurityTransactions")
            .HasPartitionKey(x=> x.CdsIdentifier);
        }
    }
}