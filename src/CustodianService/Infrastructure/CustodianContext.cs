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
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPortfolio> CustomerAssets { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseCosmos(
            "https://localhost:8081",
            "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==",
            databaseName: "CustodianDB");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultContainer("Customers");

            modelBuilder.Entity<Customer>().ToContainer("Customers").HasPartitionKey(x=> x.CdsIdentifier);
            modelBuilder.Entity<CustomerPortfolio>().ToContainer("CustomerPortfolio").HasPartitionKey(x=> x.CdsIdentifier);
        }
    }
}