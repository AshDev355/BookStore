using System.Diagnostics;
using System.Linq;
using BooksStore.Models;
using BookStore.Models;
using BookStore.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace BookStore.Controllers
{
    public class HomeController : Controller
    {
        private static List<Author> authors = new List<Author>
        {
            new Author {Id = 1, Name = "John Smith", Country = "USA"},
            new Author {Id = 2, Name = "ALi Ahmed", Country ="Pakistan" }
        };


        private static List<Book> books = new List<Book>
        {
            new Book { Id = 1, Title = "C# Programming", Price = 1500, AuthorId = 1 },
            new Book { Id = 2, Title = "ASP.NET Core Guide", Price = 2000, AuthorId = 2 },
            new Book { Id = 3, Title = "Learn MVC", Price = 1800, AuthorId = 1 }
        };

        private static int GetNextAuthorId()
        {
            return authors.Count == 0 ? 1 : authors.Max(a => a.Id) + 1;
        }

        private static int GetNextBookId()
        {
            return books.Count == 0 ? 1 : books.Max(b => b.Id) + 1;
        }

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            var viewModel = new HomeViewModel
            {
                Books = books.Select(book => new BookCardViewModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Price = book.Price,

                    AuthorName = authors
                        .FirstOrDefault(a => a.Id == book.AuthorId)?.Name ?? "Unknown"
                }).ToList(),

                Authors = authors
            };

            return View(viewModel);
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        public IActionResult AddBook(string title, decimal price, int authorId)
        {
            if (string.IsNullOrWhiteSpace(title))
            {
                return RedirectToAction("Index");
            }

            books.Add(new Book
            {
                Id = GetNextBookId(),
                Title = title,
                Price = price,
                AuthorId = authorId
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddAuthor(string name, string country)
        {
            authors.Add(new Author
            {
                Id = GetNextAuthorId(),
                Name = name,
                Country = country
            });

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveBook(int id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);

            if (book != null)
            {
                books.Remove(book);
            }

            return RedirectToAction("Index");
        }
    }
}
