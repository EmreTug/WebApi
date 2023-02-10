using AutoMapper;
using BookStore.Applications.GenreOperations.Command.CreateGenre;
using BookStore.Applications.GenreOperations.Command.DeleteGenre;
using BookStore.Applications.GenreOperations.Command.UpdateGenre;
using BookStore.Applications.GenreOperations.Queries.GetGenreDetail;
using BookStore.Applications.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Applications.GenreOperations.Command.CreateGenre.CreateGenreCommand;
using static BookStore.Applications.GenreOperations.Command.UpdateGenre.UpdateGenreCommand;
using static BookStore.Applications.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;


namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            GenreDetailViewModel result;
            try
            {
                query.genreId = id;
                GetGenreDetailQueryValidator validator = new GetGenreDetailQueryValidator();
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
        public IActionResult AddGenre(CreateGenreModel newGenre)
        {
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            try
            {
                command.model = newGenre;
                CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
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
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            try
            {
                command.genreId = id;
                command.updateGenre = updateGenre;
                UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
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
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            try
            {
                command.Id = id;
                DeleteGenreCommandValidator validation = new DeleteGenreCommandValidator();
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