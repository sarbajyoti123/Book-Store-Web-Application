using System.Collections.Generic;
using testingGithub.Models;
using testingGithub.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace testingGithub.Repository
{
    public class BookRepo
    {

        BookStoreContext db = new BookStoreContext();

        public async Task<int> AddNewBook(BookModel model)
        {
            Books bk = new Books();
            bk.Author = model.Author;
            bk.Title = model.Title;
            bk.Description = model.Description;
            bk.CreatedOn = System.DateTime.UtcNow;
            bk.UpdatedOn = System.DateTime.UtcNow;
            
            bk.Category = model.Category;

            bk.CoverImageUrl = model.CoverImageUrl;
            
            if(model.TotalPages > 0)
            {
                bk.TotalPages = model.TotalPages;
            }
            else { bk.TotalPages = 0; }
            bk.Language = model.Language;

            bk.bookGaleries = new List<BookGalery>();

            foreach(var file in model.Gallery)
            {
                bk.bookGaleries.Add(new BookGalery()
                {
                    //BookId = file.Id,
                    Name = file.Name,
                    URL = file.URL

                });
            }


            await db.Books.AddAsync(bk);
            await db.SaveChangesAsync();

            return bk.Id;
        }





        public void Delete(BookModel model)
        {
            var del = db.Books.Find(model.Id);
            if (del != null)
            {
                db.Books.Remove(del);
                db.SaveChanges();

            }
        }



        public void EditData(BookModel model)
        {
            var data = db.Books.Find(model.Id);
            data.Title = model.Title;
            data.Description = model.Description;
            data.UpdatedOn = System.DateTime.UtcNow;
            data.Category = model.Category;
            data.Language = model.Language;
            data.TotalPages = model.TotalPages;
            data.Author = model.Author;

            db.Books.Attach(data);
            db.SaveChanges();
        }








        public async Task<List<BookModel>>  GetBooks()
        {

            
            var books = new List<BookModel>();
            List<Books> dlist = await db.Books.ToListAsync();
            if(dlist != null)
            {
                foreach (var book in dlist)
                {
                    books.Add(new BookModel() { Id = book.Id,CoverImageUrl = book.CoverImageUrl, CreatedOn = book.CreatedOn, UpdatedOn = book.UpdatedOn, Author = book.Author, Title = book.Title, Description = book.Description, Category = book.Category, TotalPages = book.TotalPages, Language = book.Language });

                }
            }
            


            return books;
        }

        //public List<string> BookLanguage()
        //{
        //    var lang = new List<string>();
        //    var data = db.Books.ToList();
        //    foreach (var book in data)
        //    {
        //        lang.Add(book.Language);
        //    }

        //    return lang;

        //}


        





        public async Task<BookModel>  BookId(int id)
        {

            //return BookCollection().Find(E => E.Id == id);

            var data = await db.Books.FindAsync(id);
            if (data != null)
            {
                return new BookModel() { Id = data.Id, CoverImageUrl = data.CoverImageUrl, Author = data.Author, Title = data.Title, Description = data.Description, Category = data.Category, TotalPages = data.TotalPages, Language = data.Language, Gallery = data.bookGaleries.Select(g => new GalleryModel() { Id = g.Id, Name = g.Name, URL = g.URL}).ToList() }; 

                
            }

            return null;




        }


        public BookModel EditBookDetail(int id)
        {
            var book = db.Books.Find(id);
            if(book != null)
            {
                return new BookModel() { Id = book.Id,UpdatedOn= book.UpdatedOn, CreatedOn = book.CreatedOn,Author = book.Author,Title = book.Title,Description = book.Description, Category = book.Category,TotalPages = book.TotalPages,Language= book.Language};
            }
            return null;
        }


        



        public List<BookModel> SearchBooks(string title, string authorname)
        {
            return BookCollection().FindAll(E => E.Title.Contains(title) && E.Author.Contains(authorname));
        }


        public List<BookModel> BookCollection()
        {



            var books = new List<BookModel>();
            books.Add(new BookModel() { Id = 0, Title = "C#", Author = "Sarbajyoti", Description = "This is a C# book", Category = "Coding", TotalPages = 323, Language = "English" });
            books.Add(new BookModel() { Id = 1, Title = "Python", Author = "sagar", Description = "This is a python book", Category = "pyComic", TotalPages = 450, Language = "Hindi" });
            books.Add(new BookModel() { Id = 2, Title = "Java", Author = "sam", Description = "This is a java book", Category = "jvEmotional", TotalPages = 670, Language = "Bengali" });
            //return new List<BookModel>()
            //{
            //    new BookModel(){Id = 0, Title = "C#", Author = "Sarbajyoti",Description = "This is a C# book",Category = "Coding",TotalPages = 323,Language = "English"},
            //    new BookModel(){Id = 1, Title ="Python",Author ="sagar",Description = "This is a python book",Category = "pyComic",TotalPages = 450,Language = "Hindi"},
            //    new BookModel(){Id = 2, Title ="Java",Author = "sam",Description = "This is a java book",Category = "jvEmotional",TotalPages = 670,Language = "Bengali"}

            //};

            return books;

        }

    }
}
