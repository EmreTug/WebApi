using AutoMapper;
using BookStore.Applications.BookOperations.Commands.CreateBook;
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
using static BookStore.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.UnitTests.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var author = new Author()
            {
                Name = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvalidOperationException_ShouldBeReturn",
                AuthorId = author.Id,
                GenreId = 1,
                PageCount = 10,
                PublishDate = new DateTime(2020, 7, 14)
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.model = new CreateBookModel() { Title = book.Title, AuthorId = author.Id, GenreId = book.GenreId, PageCount = book.PageCount, PublishDate = book.PublishDate };

            //act and assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Kitap kayıtlı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeAdded()
        {
            // arrange
            var author = new Author()
            {
                Name = "WhenValidInputsAreGiven_Book_ShouldBeAdded",
                Surname = "WhenValidInputsAreGiven_Book_ShouldBeAdded",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            var genre = new Genre()
            {
                Name = "WhenValidInputsAreGiven_Book_ShouldBeAdded",
            };
            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel model = new CreateBookModel()
            {
                Title = "WhenValidInputsAreGiven_Book_ShouldBeAdded",
                GenreId = genre.Id,
                PageCount = 200,
                PublishDate = new DateTime(2014, 7, 14),
                AuthorId = author.Id
            };
            command.model = model;

            // act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            //act and assert
            var book = _context.Books.SingleOrDefault(x => x.Title == model.Title && x.AuthorId == model.AuthorId);
            book.Should().NotBeNull();
            book.Title.Should().Be(model.Title);
            book.GenreId.Should().Be(model.GenreId);
            book.PageCount.Should().Be(model.PageCount);
            book.PublishDate.Should().Be(model.PublishDate);
            book.AuthorId.Should().Be(model.AuthorId);
        }


    }
}
