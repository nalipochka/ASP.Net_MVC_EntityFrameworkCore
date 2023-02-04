using ASP.Net_MVC_EntityFrameworkCore.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO;
using static System.Net.WebRequestMethods;

namespace ASP.Net_MVC_EntityFrameworkCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ILibrary<Book> library;
        private readonly IWebHostEnvironment hostEnvironment;

        public HomeController(ILogger<HomeController> logger, ILibrary<Book> library, IWebHostEnvironment hostEnvironment)
        {
            _logger = logger;
            this.library = library;
            this.hostEnvironment = hostEnvironment;
        }

        public IActionResult Library()
        {
            IEnumerable<Book> books = library.GetAll();
            return View(books);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Details(int id)
        {
            Book? book = library.Get(id);
            return View(book);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Book book, IFormFile filePath)
        {
            if (!ModelState.IsValid)
            {
                
                return Content("<html><h2 style='color: red;'>There aren`t all filled fields!</h2></html>", "text/html");
            }
            if (filePath != null)
            {
                //using(MemoryStream ms = new MemoryStream())
                //{
                //    photo.CopyTo(ms);
                //    book.Photo = ms.ToArray();
                //}
                string fileName = $"/images/{filePath.FileName}";
                string fileFullPath = hostEnvironment.WebRootPath + fileName;
                using (FileStream fs = new(fileFullPath, FileMode.Create, FileAccess.Write))
                {
                    filePath.CopyTo(fs);
                    book.FilePath = fileName;
                }
            }
            //int id = library.GetAll().Max(x => x.Id);
            //book.Id = ++id;
            library.Add(book);
            return RedirectToAction("Library");

        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if (id == null)
            {
                return Content("<html><h2 style='color: red;'>You must provide id of book!</h2></html>", "text/html");
            }
            Book? book = library.Get(id);
            return View(book);
        }
        [HttpPost]
        public IActionResult Edit(Book book, IFormFile filePath)
        {
            if (ModelState.IsValid)
            {
                if (filePath != null)
                {
                    //using (MemoryStream ms = new MemoryStream())
                    //{
                    //    photo.CopyTo(ms);
                    //    book.Photo = ms.ToArray();
                    //}
                    string fileName = $"/images/{filePath.FileName}";
                    string fileFullPath = hostEnvironment.WebRootPath + fileName;
                    using (FileStream fs = new(fileFullPath, FileMode.Create, FileAccess.Write))
                    {
                        filePath.CopyTo(fs);
                        book.FilePath = fileName;
                    }
                }
                library.Edit(book);
                return RedirectToAction("Library");
            }
            return View(book);

        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {

                return Content("<html><h2 style='color: red;'>You must provide id of book!</h2></html>", "text/html");
            }
            Book? book = library.Get(id.Value);
            return View(book);
        }
        [HttpPost]
        [ActionName("Delete")]
        public IActionResult ConfirmDelete(int? id)
        {
            if (library.Delete(id.Value))
            {
                return RedirectToAction("Library");
            }
            else
            {
                
                return Content($"<html><h2 style='color: red;'>The book with id = {id.Value} is not exists!</h2></html>", "text/html");
            }

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}