using AutoMapper;
using BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.authorId = -1;
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author bulunamadı");
        }

        [Fact]
        public void WhenExistAuthorIdIsGiven_AuthorDetailViewModel_ShouldBeReturn()
        {
            var author = new Author()
            {
                Name = "WhenExistAuthorIdIsGiven_AuthorDetailViewModel_ShouldBeReturn",
                Surname = "WhenExistAuthorIdIsGiven_AuthorDetailViewModel_ShouldBeReturn",
                DateOfBirth = DateTime.Now.AddYears(-1),
            };
            if (_context.Authors.FirstOrDefault(x => x.Name == author.Name && x.Surname == author.Surname)==null)
            {
                _context.Authors.Add(author);
                _context.SaveChanges();
            }

            var authorId = _context.Authors.SingleOrDefault(x => x.Name == author.Name && x.Surname == author.Surname).Id;

            GetAuthorDetailQuery query = new GetAuthorDetailQuery(_context, _mapper);
            query.authorId = authorId;

            FluentActions.Invoking(() => query.Handle()).Invoke().Should().NotBeNull();

        }
    }
}