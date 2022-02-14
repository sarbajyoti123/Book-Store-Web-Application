using Microsoft.EntityFrameworkCore;
using testingGithub.Models;

namespace testingGithub.Data
{
    public class BookStoreContext : DbContext
    {

        public DbSet<Books> Books { get; set; }
        public DbSet<BookGalery> BookGalery { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = DESKTOP-ACMKVB0\CAPGEMINI;Database = BookStore; User ID = sagar; Password = sagar@12345");
            base.OnConfiguring(optionsBuilder);
        }

        public DbSet<testingGithub.Models.BookModel> BookModel { get; set; }



    }
}
