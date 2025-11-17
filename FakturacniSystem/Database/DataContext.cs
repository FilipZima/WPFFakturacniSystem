using Microsoft.EntityFrameworkCore;
using FakturacniSystem.Models;

namespace FakturacniSystem.Database
{
    public class DataContext : DbContext
    {
        public DbSet<InvoiceEntity> InvoiceSet { get; set; }

        public DataContext() 
        { 
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source = InvoiceDB.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
