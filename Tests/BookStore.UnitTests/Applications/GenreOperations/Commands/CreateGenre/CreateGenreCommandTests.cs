using AutoMapper;
using BookStore.Applications.GenreOperations.Command.CreateGenre;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore.Applications.GenreOperations.Command.CreateGenre.CreateGenreCommand;

namespace BookStore.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var genre = new Genre()
            {
                Name = "WhenAlreadyExistGenreNameIsGiven_InvalidOperationException_ShouldBeReturn",
                
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.model = new CreateGenreModel() { Name =genre.Name };

            //act and assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre kayıtlı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeAdded()
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreModel model = new CreateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Genre_ShouldBeAdded",
                
            };
            command.model = model;

            // act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            //act and assert
            var genre = _context.Genres.SingleOrDefault(x => x.Name == model.Name);
            genre.Should().NotBeNull();
            genre.Name.Should().Be(model.Name);
            
        }


    }
}
