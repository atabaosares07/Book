using Book.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Book.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Book.Data.Entities.Book> Books { get; set; }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Publisher> Publishers { get; set; }

        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.SetTableName(entityType.DisplayName());
            }

            modelBuilder.Entity<BookAuthor>().HasKey(o => new { o.BookId, o.AuthorId });
            modelBuilder.Entity<BookAuthor>()
                .HasOne<Book.Data.Entities.Book>(sc => sc.Book)
                .WithMany(s => s.BookAuthors)
                .HasForeignKey(sc => sc.BookId);
            modelBuilder.Entity<BookAuthor>()
                .HasOne<Author>(sc => sc.Author)
                .WithMany(s => s.BookAuthors)
                .HasForeignKey(sc => sc.AuthorId);
        }
    }
}
