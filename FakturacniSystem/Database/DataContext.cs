using Microsoft.EntityFrameworkCore;

namespace FakturacniSystem.Database
{
    public class DataContext : DbContext
    {
        public DbSet<Invoice> InvoiceSet { get; set; }
        public DbSet<InvoiceItem> InvoiceItemSet { get; set; }

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
            modelBuilder.Entity<Invoice>()
            .HasMany(i => i.Items)
            .WithOne(item => item.Invoice)
            .HasForeignKey(item => item.InvoiceId)
            .IsRequired();

            base.OnModelCreating(modelBuilder);
        }
    }
}
