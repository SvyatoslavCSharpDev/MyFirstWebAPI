using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore; //Нужен для работы с Миграциями без Tools
using MyFirstWebAPI.Models;
using MyFirstWebAPI.Классы;
namespace MyFirstWebAPI.Controllers
{
    [ApiController]
    [Route("Books")] //заменяет на имя класса без Controller [controller]
    public class BooksController : ControllerBase  //В контроллере пишеться без Controller
    {
        private readonly AppDbContext _context;

        public BooksController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_context.Books.ToList());
        }

        [HttpGet("{id}")] //Будем передавать писать в () значения которые будем писать в swagger в данном случае int id
        public IActionResult GetById(int id)
        {
            //Book bookOne = new Book { Id = 1, Title = "Война и мир", Author = "Лев Толстой", Pages = 1056 }; 
            //Book bookTwo = new Book { Id = 2, Title = "Сестра Керри", Author = "Джек Лондон", Pages = 1484 };
            //List<Book> books = new List<Book>(); //Лучше сделать списо вверху программы, а не в методах. Static - нужен чтобы список не обновлялся при кажом запросе
            //books.Add(bookOne);
            //books.Add(bookTwo);

            //foreach(var book in books)
            //{
            //    if (book.Id == id)
            //    {
            //        return book;
            //    }
            //}
            var bookId = _context.Books.FirstOrDefault(x => x.Id == id);

            if(bookId == null)
            {
                return NotFound($"Нет книг с таким id:{id}");
            }
            return Ok(bookId);
           
        }
       
        [HttpPost]
        public IActionResult AddBook(Book book)
        {
           /* book.Id = _context.Books.Max(b => b.Id) + 1;*/ //IDENTITY в базе данных говорит, не Лезь! Я сама выставляю ID
            _context.Books.Add(book);
            _context.SaveChanges(); // ← ОБЯЗАТЕЛЬНО
            return CreatedAtAction(nameof(GetById), new { id = book.Id }, book);
        }

        [HttpPut ("{id}")]
        public IActionResult ChangeBook(int id, Book book)
        {
            var newbook = _context.Books.FirstOrDefault(x => x.Id == id);

            if(newbook == null)
            {
                return NotFound($"Книга с id {id} ненайдена!");
            }

            newbook.Title = book.Title;
            newbook.Author = book.Author;
            newbook.Pages = book.Pages;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var deleteBook = _context.Books.FirstOrDefault(x => x.Id == id); //FirstOrDefault - возвращает только одну книгу первую найденную, Where - возвращает несколько подходящих под условие
            if (deleteBook == null)
            {
                return NotFound($"Нет книги с id: {id}");
            }
            _context.Books.Remove(deleteBook);
            return Ok(deleteBook);
        }

        [HttpGet("search")]
        public IActionResult SearchBookByAuthor(string author)
        {
            var booksearch = _context.Books.Where(x =>x.Author.Contains(author, StringComparison.OrdinalIgnoreCase));
            
            return Ok(booksearch);
          
        }
    }
}
