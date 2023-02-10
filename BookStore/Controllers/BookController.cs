using AutoMapper;
using BookStore.Applications.BookOperations.Commands.CreateBook;
using BookStore.Applications.BookOperations.Commands.DeleteBook;
using BookStore.Applications.BookOperations.Commands.UpdateBook;
using BookStore.Applications.BookOperations.Queries.GetBookDetail;
using BookStore.Applications.BookOperations.Queries.GetBooks;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using static BookStore.Applications.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public BookController(IBookStoreDbContext context,IMapper mapper)
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
                GetBookDetailQueryValidator validator = new GetBookDetailQueryValidator();
                validator.ValidateAndThrow(query);
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
                CreateBookCommandValidator validator = new CreateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
                validator.ValidateAndThrow(command);
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
                DeleteBookCommandValidator validation= new DeleteBookCommandValidator();
                validation.ValidateAndThrow(command);
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