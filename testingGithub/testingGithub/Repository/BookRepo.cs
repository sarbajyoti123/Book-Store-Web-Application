using System.Collections.Generic;
using testingGithub.Models;
using testingGithub.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Grpc.Core;

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

            bk.Useridbook = model.Useridbookss;
            
            bk.Category = model.Category;

            bk.CoverImageUrl = model.CoverImageUrl;
            
            if(model.TotalPages > 0)
            {
                bk.TotalPages = model.TotalPages;
            }
            else { bk.TotalPages = 0; }
            bk.Language = model.Language;

            bk.PDFUrl = model.PDFURL;

            bk.BookGalery = new List<BookGalery>();

            foreach(var file in model.Gallery)
            {
                bk.BookGalery.Add(new BookGalery()
                {
                    //BookId = file.Id,
                    Name = file.Name,
                    URL = file.URL,
                    

                });
            }


            await db.Books.AddAsync(bk);
            await db.SaveChangesAsync();

            return bk.Id;
        }





        public void  Delete(BookModel model)
        {

            var del = db.Books.Find(model.Id);

            if (del != null)
            {
                db.Books.Remove(del);
                db.SaveChanges();

                //var data = db.Books.Where(x => x.Id == model.Id)
                // .Select(book => new BookModel()
                // {

                //     Gallery = book.BookGalery.Select(g => new GalleryModel()
                //     {

                //         URL = g.URL
                //     }).ToList()
                // }).FirstOrDefault();

                //foreach (var book in data.Gallery)
                //{
                //    System.IO.File.Delete(@"C:\Users\HP\source\repos\testingVisualStudio\testingGithub\testingGithub\wwwroot\" + book.URL);
                //}



            }





        }



        public void EditData(BookModel model)
        {
            //var data = db.Books.Find(model.Id);
            //data.Title = model.Title;
            //data.Description = model.Description;
            //data.UpdatedOn = System.DateTime.UtcNow;
            //data.Category = model.Category;
            //data.Language = model.Language;
            //data.TotalPages = model.TotalPages;
            //data.Author = model.Author;

            //db.Books.Attach(data);
            //db.SaveChanges();

            var datas = db.Books.Where(x => x.Id == model.Id).FirstOrDefault();
            if (datas != null)
            {
                datas.Id = model.Id;
                datas.Title = model.Title;
                datas.Description = model.Description;
                datas.Author = model.Author;
                datas.Category = model.Category;
                datas.Language = model.Language;
                datas.TotalPages = model.TotalPages;
                datas.Language = model.Language;

                datas.BookGalery = new List<BookGalery>();

                if(model.CoverImageUrl != null)
                {
                    datas.CoverImageUrl = model.CoverImageUrl;
                }

                if(model.PDFURL != null)
                {
                    datas.PDFUrl = model.PDFURL;
                }
                if(model.Gallery != null)
                {
                    foreach (var file in model.Gallery)
                    {
                        datas.BookGalery.Add(new BookGalery()
                        {
                            //BookId = file.Id,
                            Name = file.Name,
                            URL = file.URL,


                        });

                    }
                }

                

            }

            db.Books.Attach(datas);
            db.SaveChanges();

        }

        public BookModel EditBookDetail(int id)
        {


            return db.Books.Where(X => X.Id == id).Select(book => new BookModel()
            {
                Id = book.Id,
                UpdatedOn = book.UpdatedOn,
                CreatedOn = book.CreatedOn,
                Title = book.Title,
                Description = book.Description,
                Category = book.Category,
                Language = book.Language,
                TotalPages = book.TotalPages,
                Author = book.Author,
                PDFURL = book.PDFUrl,
                Gallery = book.BookGalery.Select(x => new GalleryModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    URL = x.URL
                }).ToList()
            }).FirstOrDefault();


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

        public async Task<List<BookModel>> GetTopBooks(int count)
        {
            return await db.Books.Select(books => new BookModel()
            {
                Id = books.Id,
                CoverImageUrl = books.CoverImageUrl,
                CreatedOn = books.CreatedOn,
                UpdatedOn = books.UpdatedOn,
                Author = books.Author,
                Title = books.Title,
                Description = books.Description,
                Category = books.Category,
                TotalPages = books.TotalPages,
                Language = books.Language

            }).Take(count).ToListAsync();
        }


        public async Task<List<BookModel>> GetSimilarBooks(int id)
        {
            var data = db.Books.Where(x => x.Id == id).FirstOrDefault();
            var author = data.Author;
            return await db.Books.Where(Books => Books.Author == author).Select(datas => new BookModel()
            {
                Id = datas.Id,
                CoverImageUrl = datas.CoverImageUrl,
                CreatedOn = datas.CreatedOn,
                UpdatedOn = datas.UpdatedOn,
                Author = datas.Author,
                Title = datas.Title,
                Description = datas.Description,
                Category = datas.Category,
                TotalPages = datas.TotalPages,
                Language = datas.Language
            }).ToListAsync();
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


        





        public BookModel BookId(int id)
        {

            //return BookCollection().Find(E => E.Id == id);

            return db.Books.Where(x => x.Id == id)
                 .Select(book => new BookModel()
                 {
                     Author = book.Author,
                     Category = book.Category,
                     Description = book.Description,
                     Id = book.Id,
                     //LanguageId = book.LanguageId,
                     Language = book.Language,
                     Useridbookss = book.Useridbook,
                     Title = book.Title,
                     TotalPages = book.TotalPages,
                     CoverImageUrl = book.CoverImageUrl,
                     PDFURL = book.PDFUrl,
                     Gallery = book.BookGalery.Select(g => new GalleryModel()
                     {
                         Id = g.Id,
                         Name = g.Name,
                         URL = g.URL
                     }).ToList()
                 }).FirstOrDefault();


            //var data = db.Books.Where(x => x.Id == id);
            //var datalist = new List<BookModel>();
            //foreach (var book in data)
            //{
            //    datalist.Add(new BookModel()
            //    {
            //        Author = book.Author,
            //        Title = book.Title,
            //        Category = book.Category,
            //        Description = book.Description,
            //        TotalPages = book.TotalPages,
            //        Id = book.Id,
            //        Language = book.Language,
            //        CoverImageUrl = book.CoverImageUrl,
            //        Gallery = book.BookGalery.Select(g => new GalleryModel()
            //        {
            //            Id = g.Id,
            //            Name = g.Name,
            //            URL = g.URL,
            //        }).ToList()
            //    });
            //}
            //return datalist;


        }









        






        //public List<BookModel> SearchBooks(string title, string authorname)
        //{
        ////return BookCollection().FindAll(E => E.Title.Contains(title) && E.Author.Contains(authorname));
        //    return null;
        //}


        //public List<BookModel> BookCollection()
        //{



        //    var books = new List<BookModel>();
        //    books.Add(new BookModel() { Id = 0, Title = "C#", Author = "Sarbajyoti", Description = "This is a C# book", Category = "Coding", TotalPages = 323, Language = "English" });
        //    books.Add(new BookModel() { Id = 1, Title = "Python", Author = "sagar", Description = "This is a python book", Category = "pyComic", TotalPages = 450, Language = "Hindi" });
        //    books.Add(new BookModel() { Id = 2, Title = "Java", Author = "sam", Description = "This is a java book", Category = "jvEmotional", TotalPages = 670, Language = "Bengali" });
        //    //return new List<BookModel>()
        //    //{
        //    //    new BookModel(){Id = 0, Title = "C#", Author = "Sarbajyoti",Description = "This is a C# book",Category = "Coding",TotalPages = 323,Language = "English"},
        //    //    new BookModel(){Id = 1, Title ="Python",Author ="sagar",Description = "This is a python book",Category = "pyComic",TotalPages = 450,Language = "Hindi"},
        //    //    new BookModel(){Id = 2, Title ="Java",Author = "sam",Description = "This is a java book",Category = "jvEmotional",TotalPages = 670,Language = "Bengali"}

        //    //};

        //    return books;

        //}

    }
}
