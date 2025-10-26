using Microsoft.EntityFrameworkCore;
using Mini_Expense_Tracker.Models;

namespace Mini_Expense_Tracker.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Expense> Expenses => Set<Expense>();
        public DbSet<Category> Categories => Set<Category>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Food" },
                new Category { Id = 2, Name = "Transport" },
                new Category { Id = 3, Name = "Utilities" },
                new Category { Id = 4, Name = "Entertainment" },
                new Category { Id = 5, Name = "Health" }
            );
            // Expense
            modelBuilder.Entity<Expense>(b =>
            {
                b.HasKey(e => e.Id);
                b.Property(e => e.Amount).HasColumnType("decimal(10,2)");
                b.Property(e => e.Description).HasMaxLength(100);
                b.Property(e => e.Note).HasMaxLength(250);

                b.HasOne(e => e.Category)
                 .WithMany(c => c.Expenses)
                 .HasForeignKey(e => e.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);

                // Helpful indexes
                b.HasIndex(e => e.When);
                b.HasIndex(e => new { e.CategoryId, e.When });
            });

            // Category
            modelBuilder.Entity<Category>(b =>
            {
                b.HasKey(c => c.Id);
                b.Property(c => c.Name).IsRequired().HasMaxLength(50);
                b.HasIndex(c => c.Name).IsUnique(); // optional uniqueness
            });

        }
    }
}
