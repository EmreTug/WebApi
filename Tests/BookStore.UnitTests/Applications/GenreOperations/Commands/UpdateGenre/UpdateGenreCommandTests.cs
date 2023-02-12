using FluentAssertions;
using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Applications.BookOperations.Commands.UpdateBook;
using Moq;
using Xunit;
using BookStore.Entities;
using System.Collections.Generic;
using BookStore.UnitTests.TestSetup;
using static BookStore.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using BookStore.Applications.GenreOperations.Command.UpdateGenre;
using static BookStore.Applications.GenreOperations.Command.UpdateGenre.UpdateGenreCommand;

namespace BookStore.UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;


        public UpdateGenreCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId = 555;
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre bulunamadı");
        }
        [Fact]

        public void WhenExistGenreIdAndUpdateGenreModelAreGiven_Genre_ShouldBeUpdated()
        {
            var genre = new Genre()
            {
                Name = "WhenExistGenreIdAndUpdateGenreModelAreGiven_Genre_ShouldBeUpdated",
             
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();



            var GenreId = _context.Genres.SingleOrDefault(x => x.Name == genre.Name).Id;

            var updateModel = new UpdateGenreModel()
            {
               Name=genre.Name+"update",
            };
            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.genreId=GenreId;
            command.updateGenre=updateModel;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            _context.Genres.Any(x => x.Id == GenreId
            && x.Name==updateModel.Name
            ).Should().BeTrue();
        }
    }
}