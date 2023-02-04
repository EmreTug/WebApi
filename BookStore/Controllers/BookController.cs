using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.GetBooks;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.GetBooks.GetBooksQuery;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        public BookController(BookStoreDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return _context.Books.FirstOrDefault(book=>book.Id==id);
        }
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    return BookList.FirstOrDefault(book => book.Id == Convert.ToInt16(id));
        //}
        [HttpPost]
        public IActionResult AddBook(CreateBookModel newBook) {
            CreateBookCommand command = new CreateBookCommand(_context);
            try
            {
                command.model = newBook;

                command.Handle();

            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody]Book updateBok) {
            var result = _context.Books.FirstOrDefault(a => a.Id == id);
            if (result is null)
                return BadRequest();
            result.PublishDate = updateBok.PublishDate!=default?updateBok.PublishDate:result.PublishDate;
            result.GenreId = updateBok.GenreId != default ? updateBok.GenreId : result.GenreId;
            result.Title=updateBok.Title != default ? updateBok.Title : result.Title;
            result.PageCount=updateBok.PageCount != default ? updateBok.PageCount : result.PageCount;
            _context.SaveChanges();
            return Ok();
            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = _context.Books.FirstOrDefault(a => a.Id == id);
            if (result is null)
                return BadRequest();
            _context.Books.Remove(result);
            _context.SaveChanges();

            return Ok();

        }


    }
}