using Core;
using Microsoft.EntityFrameworkCore;

namespace DB
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }

        public ApplicationContext()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>().Property(b => b.Name).HasMaxLength(30).IsRequired();
            modelBuilder.Entity<Book>().Property(b => b.Year).IsRequired();

            modelBuilder.Entity<Author>().HasData(new Author { AuthorId = 1, LastName = "Гоголь" });
            modelBuilder.Entity<Author>().HasData(new Author { AuthorId = 2, LastName = "Пушкин" });
            modelBuilder.Entity<Author>().HasData(new Author { AuthorId = 3, LastName = "Блок" });

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=BookCatalogDB;Trusted_Connection=True;");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}