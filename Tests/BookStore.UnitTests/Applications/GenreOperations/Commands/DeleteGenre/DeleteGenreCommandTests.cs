using BookStore.Applications.BookOperations.Commands.DeleteBook;
using BookStore.Applications.GenreOperations.Command.DeleteGenre;
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

namespace BookStore.UnitTests.Applications.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.Id = 555;
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre Bulunamadı");
        }
        [Fact]
        public void WhenExistGenreIdIsGiven_Genre_ShouldBeDeleted()
        {
            var genre = new Genre()
            {
                Name = "WhenExistGenreIdIsGiven_Genre_ShouldBeDeleted",
               
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var id = _context.Genres.FirstOrDefault(g => g.Name == genre.Name).Id;
            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.Id = id;
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            genre = _context.Genres.SingleOrDefault(x => x.Id == id);
            genre.Should().BeNull();
        }

    }
}