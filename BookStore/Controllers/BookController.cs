using AutoMapper;
using BookStore.BookOperations.CreateBook;
using BookStore.BookOperations.DeleteBook;
using BookStore.BookOperations.GetBookDetail;
using BookStore.BookOperations.GetBooks;
using BookStore.BookOperations.UpdateBook;
using BookStore.DBOperations;
using Microsoft.AspNetCore.Mvc;
using static BookStore.BookOperations.CreateBook.CreateBookCommand;
using static BookStore.BookOperations.GetBookDetail.GetBookDetailQuery;
using static BookStore.BookOperations.GetBooks.GetBooksQuery;
using static BookStore.BookOperations.UpdateBook.UpdateBookCommand;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context,_mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context,_mapper);
            BookDetailViewModel result;
            try
            {
                query.bookId = id;
                result = query.Handle();

            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            return Ok(result);

        }
     
        [HttpPost]
        public IActionResult AddBook(CreateBookModel newBook) {
            CreateBookCommand command = new CreateBookCommand(_context,_mapper);
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
        public IActionResult UpdateBook(int id,[FromBody] UpdateBookModel updateBok) {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            try
            {
                command.bookId = id;
                command.updateBok = updateBok;
                command.Handle();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
            return Ok();
            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            try
            {
                command.Id = id;
                command.Handle();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
                
            }

            return Ok();

        }


    }
}