using BookStore.Applications.BookOperations.Commands.DeleteBook;
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

namespace BookStore.UnitTests.Applications.BookOperations.Command.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = 555;
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap Bulunamadı");
        }
        [Fact]
        public void WhenExistBookIdIsGiven_Book_ShouldBeDeleted()
        {
            var book = new Book()
            {
                Title = "WhenExistBookIdIsGiven_Book_ShouldBeDeleted",
                GenreId = 2,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1),
                AuthorId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            var id = _context.Books.FirstOrDefault(b => b.Title == book.Title).Id;
            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.Id = id;
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            book = _context.Books.SingleOrDefault(x => x.Id == id);
            book.Should().BeNull();
        }

    }
}