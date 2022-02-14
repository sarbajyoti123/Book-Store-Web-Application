using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using testingGithub.Models;
using testingGithub.Repository;
using testingGithub.Service;

namespace Webgentle.BookStore.Controllers
{
    public class BookController : Controller
    {

        BookRepo br = new BookRepo();
        //private readonly BookRepository _bookRepository = null;
        //private readonly LanguageRepository _languageRepository = null;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserService _userService;
        public BookController(IWebHostEnvironment webHostEnvironment,UserService userService)
        {
            
            _webHostEnvironment = webHostEnvironment;
            _userService = userService;
        }

        public async Task<ViewResult> GetAllBooks()
        {
            var data = await br.GetBooks();

            return View(data);
        }

        [Route("book-details/{id}", Name = "bookDetailsRoute")]
        public ViewResult GetBook(int id)
        {
            var data = br.BookId(id);

            //ViewBag.similarbooks = br.GetSimilarBooks(id);



            return View(data);
        }

        //public List<BookModel> SearchBooks(string bookName, string authorName)
        //{
        //    return br.SearchBooks(bookName, authorName);
        //}


        [Authorize]
        public  ViewResult AddBook(bool isSuccess = false, int bookId = 0)
        {
            var model = new BookModel();

            ViewBag.Language = new List<string>() { "Hindi", "English", "Bengali" };

            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookModel bookModel)
        {
            
            if (ModelState.IsValid)
            {

                bookModel.Useridbookss =  _userService.GetUserId();

                //For Uploading Cover Photo
                if (bookModel.CoverPhoto != null)
                {
                    string folder = "books/cover/";
                    bookModel.CoverImageUrl = await UploadImage(folder, bookModel.CoverPhoto);
                }

                //For Uploading Multiple photos in Detailed Product Page using One to Many Relationship
                if (bookModel.GalleryFiles != null)
                {
                    string folder = "books/gallery/";

                    bookModel.Gallery = new List<GalleryModel>();

                    foreach (var file in bookModel.GalleryFiles)
                    {
                        var gallery = new GalleryModel()
                        {
                            Name = file.FileName,
                            URL = await UploadImage(folder, file)
                        };
                        bookModel.Gallery.Add(gallery);
                    }
                }


                if(bookModel.PDFFile != null)
                {
                    string folder = "books/pdf/";
                    bookModel.PDFURL = await UploadImage(folder, bookModel.PDFFile);
                }

                int id = await br.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction("AddBook", new { isSuccess = true, bookId = id });


                };
            }

            ViewBag.Language = new List<string>() { "Hindi", "English", "Bengali" };


            return View();
        }

        private async Task<string> UploadImage(string folderPath, IFormFile file)
        {

            folderPath += Guid.NewGuid().ToString() + "_" + file.FileName;

            string serverFolder = Path.Combine(_webHostEnvironment.WebRootPath, folderPath);

            var data =  file.CopyToAsync(new FileStream(serverFolder, FileMode.Create));
            
            await data;
            //data.Dispose();
            return "/" + folderPath;

        }

        [Authorize]
        public ViewResult DeleteBook(int id)
        {
            ViewBag.deleteid = id;
            return View();
        }

        [HttpPost]
        public ViewResult DeleteBook(BookModel model)
        {
            if (model != null)
            {
                br.Delete(model);
            }

            return View();
        }

        [Authorize]
        public ViewResult EditBook(int id)
        {
            ViewBag.Language = new List<string>() { "Hindi", "English", "Bengali" };
            var data = br.EditBookDetail(id);
            return View(data);
        }

        [HttpPost]
        public async Task<ViewResult>  EditBook(BookModel model)
        {
            if(model.CoverPhoto != null)
            {
                string folder = "books/cover/";
                model.CoverImageUrl = await UploadImage(folder, model.CoverPhoto);
            }

            if(model.PDFFile != null)
            {
                string folder = "books/pdf/";
                model.PDFURL = await UploadImage(folder, model.PDFFile);
            }
            
            if(model.GalleryFiles != null)
            {
                string folder2 = "books/gallery/";

                model.Gallery = new List<GalleryModel>();

                foreach (var file in model.GalleryFiles)
                {
                    var gallery = new GalleryModel()
                    {
                        Name = file.FileName,
                        URL = await UploadImage(folder2, file)
                    };
                    model.Gallery.Add(gallery);
                }
            }
            
            br.EditData(model);
            ViewBag.Language = new List<string>() { "Hindi", "English", "Bengali" };
            return View();
        }


    }
}