using BookStore.Applications.AuthorOperations.Commands.DeleteAuthor;
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

namespace BookStore.UnitTests.Applications.AuthorOperations.Command.DeleteAuthor
{
    public class DeleteAuthorCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Fact]
        public void WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = 555;

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author Bulunamadı");
        }
        [Fact]
        public void WhenExistAuthorIdIsGivenAndAuthorHasNoBooks_Author_ShouldBeDeleted()
        {
            var author = new Author()
            {
                Name = "WhenExistAuthorIdIsGivenAndAuthorHasNoBooks_Author_ShouldBeDeleted",
                Surname = "WhenExistAuthorIdIsGivenAndAuthorHasNoBooks_Author_ShouldBeDeleted",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var id = _context.Authors.FirstOrDefault(a => a.Name == author.Name && a.Surname == author.Surname).Id;
            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = id;
            FluentActions
               .Invoking(() => command.Handle()).Invoke();

            author = _context.Authors.SingleOrDefault(x => x.Id == id);
            author.Should().BeNull();

        }
        [Fact]
        public void WhenExistAuthorIdIsGivenAndAuthorHasBooks_InvalidOperationException_ShouldBeReturn()
        {
            var author = new Author()
            {
                Name = "WhenExistAuthorIdIsGivenAndAuthorHasBooks_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenExistAuthorIdIsGivenAndAuthorHasBooks_InvalidOperationException_ShouldBeReturn",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var id = _context.Authors.FirstOrDefault(a => a.Name == author.Name && a.Surname == author.Surname).Id;
            var book = new Book()
            {
                Title = "WhenExistAuthorIdIsGivenAndAuthorHasBooks_InvalidOperationException_ShouldBeReturn",
                GenreId = 2,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1),
                AuthorId = id
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.Id = id;

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kullanıcı silinemez kayıtlı kitap var");



        }


 }
}
