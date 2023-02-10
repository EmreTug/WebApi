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

namespace BookStore.UnitTests.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;


        public UpdateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.bookId = 555;
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }
        [Fact]

        public void WhenExistBookIdAndUpdateBookModelAreGiven_Book_ShouldBeUpdated()
        {
            var author = new Author()
            {
                Name = "WhenExistBookIdAndUpdateBookModelAreGiven_Book_ShouldBeUpdated",
                Surname = "WhenExistBookIdAndUpdateBookModelAreGiven_Book_ShouldBeUpdated",
                DateOfBirth = DateTime.Now.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var genre = new Genre()
            {
                Name = "TestGenre"
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            var book = new Book()
            {
                Title = "WhenExistBookIdAndUpdateBookModelAreGiven_Book_ShouldBeUpdated",
                PageCount = 10,
                PublishDate = DateTime.Now.AddYears(-2),
                AuthorId = author.Id,
                GenreId = genre.Id
            };
            _context.Books.Add(book);
            _context.SaveChanges();


            var bookId = _context.Books.SingleOrDefault(x => x.Title == book.Title).Id;

            var updateModel = new UpdateBookModel()
            {
                Title = "Updated",
                PageCount = book.PageCount + 1,
                PublishDate = book.PublishDate.AddYears(-1),
                GenreId = genre.Id
            };
            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.bookId = bookId;
            command.updateBok = updateModel;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();
            _context.Books.Any(x => x.Id == bookId
            && x.Title == updateModel.Title
            && x.PageCount == updateModel.PageCount
            && x.PublishDate == updateModel.PublishDate
            && x.GenreId == updateModel.GenreId
            ).Should().BeTrue();
        }
    }
}