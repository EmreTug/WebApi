using AutoMapper;
using BookStore.Applications.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;

namespace BookStore.UnitTests.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotExistGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.genreId = -1;
            FluentActions
                .Invoking(() => query.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Genre bulunamadı");
        }

        [Fact]
        public void WhenExistGenreIdIsGiven_GenreDetailViewModel_ShouldBeReturn()
        {
            var genre = new Genre()
            {
                Name = "WhenExistGenreIdIsGiven_GenreDetailViewModel_ShouldBeReturn",
               
            };
            if (_context.Genres.FirstOrDefault(x => x.Name == genre.Name) == null)
            {
                _context.Genres.Add(genre);
                _context.SaveChanges();
            }

            var genreId = _context.Genres.SingleOrDefault(x => x.Name == genre.Name).Id;

            GetGenreDetailQuery query = new GetGenreDetailQuery(_context, _mapper);
            query.genreId = genreId;

            FluentActions.Invoking(() => query.Handle()).Invoke().Should().NotBeNull();

        }
    }
}