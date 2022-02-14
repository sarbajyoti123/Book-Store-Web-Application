using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using testingGithub.Models;

namespace testingGithub.Data
{
    public class BookStoreContext : IdentityDbContext<ApplicationUser>
    {

        public DbSet<Books> Books { get; set; }
        public DbSet<BookGalery> BookGalery { get; set; }

        
        public DbSet<ContactPage> ContactPage { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server = DESKTOP-ACMKVB0\CAPGEMINI;Database = BookFacebook; User ID = sagar; Password = sagar@12345");
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<testingGithub.Models.SignUpUserModel> SignUpUserModel { get; set; }


        public DbSet<testingGithub.Models.LoginModel> LoginModel { get; set; }


        public DbSet<testingGithub.Models.ForgetPasswordModel> ForgetPasswordModel { get; set; }


        public DbSet<testingGithub.Models.ForgetPasswordddModel> ForgetPasswordddModel { get; set; }


        public DbSet<testingGithub.Models.ResetPasswordModel> ResetPasswordModel { get; set; }

        //public DbSet<testingGithub.Models.BookModel> BookModel { get; set; }

        //public DbSet<testingGithub.Models.GalleryModel> GalleryModel { get; set; }



    }
}
