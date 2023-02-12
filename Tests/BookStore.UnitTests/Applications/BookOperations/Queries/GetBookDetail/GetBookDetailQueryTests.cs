using AutoMapper;
using BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Applications.BookOperations.Queries.GetBookDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.bookId = -1;
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap bulunamadı");
        }

        [Fact]
        public void WhenExistBookIdIsGiven_BookDetailViewModel_ShouldBeReturn()
        {
            var book = new Book()
            {
                Title = "WhenExistBookIdIsGiven_BookDetailViewModel_ShouldBeReturn",
                GenreId = 1,
                PublishDate = DateTime.Now.AddYears(-1),
                AuthorId=1,
                PageCount=1,
            };
            if (_context.Books.FirstOrDefault(x => x.Title == book.Title ) == null)
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }

            var bookId = _context.Books.SingleOrDefault(x => x.Title == book.Title ).Id;

            GetBookDetailQuery query = new GetBookDetailQuery(_context, _mapper);
            query.bookId = bookId;

            FluentActions.Invoking(() => query.Handle()).Invoke().Should().NotBeNull();

        }
    }
}