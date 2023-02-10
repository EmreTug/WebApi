using AutoMapper;
using BookStore.Applications.AuthorOperations.Commands.CreateAuthor;
using BookStore.DBOperations;
using BookStore.Entities;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Linq;
using Xunit;
using static BookStore.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace BookStore.UnitTests.Applications.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange
            var author = new Author()
            {
                Name = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                Surname = "WhenAlreadyExistAuthorNameIsGiven_InvalidOperationException_ShouldBeReturn",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.model = new CreateAuthorModel() { Name = author.Surname, Surname = author.Surname, DateOfBirth = author.DateOfBirth };

            //act and assert
            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author kayıtlı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeAdded()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel model = new CreateAuthorModel()
            {
                Name = "WhenValidInputsAreGiven_Author_ShouldBeAdded",
                Surname = "WhenValidInputsAreGiven_Author_ShouldBeAdded",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            command.model = model;

            // act
            FluentActions
            .Invoking(() => command.Handle()).Invoke();

            //act and assert
            var author = _context.Authors.SingleOrDefault(x => x.Name == model.Name && x.Surname == model.Surname);
            author.Should().NotBeNull();
            author.Name.Should().Be(model.Name);
            author.Surname.Should().Be(model.Surname);
            author.DateOfBirth.Should().Be(model.DateOfBirth);
        }


    }
}
