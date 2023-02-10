using AutoMapper;
using BookStore.Applications.AuthorOperations.Commands.CreateAuthor;
using BookStore.Applications.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Applications.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Applications.AuthorOperations.Queries.GetAuthors;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Applications.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;
using static BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;


namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery query = new GetAuthorsQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            AuthorDetailViewModel result;
            try
            {
                query.authorId = id;
                GetAuthorDetailQueryValidator validator = new GetAuthorDetailQueryValidator();
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
        public IActionResult AddAuthor(CreateAuthorModel newAuthor)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            try
            {
                command.model = newAuthor;
                CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
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
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            try
            {
                command.authorId = id;
                command.updateAuthor = updateAuthor;
                UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
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
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            try
            {
                command.Id = id;
                DeleteAuthorCommandValidator validation = new DeleteAuthorCommandValidator();
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