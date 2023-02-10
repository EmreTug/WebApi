using BookStore.Applications.BookOperations.Commands.CreateBook;
using FluentAssertions;
using System;
using Xunit;
using static BookStore.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.UnitTests.Applications.BookOperations.Command.CreateBook
{
    public class CreateBookCommandValidatorTests
    {
        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidGenreIdGiven_Validator_ShouldBeReturnError(int genreId)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                GenreId = genreId,
                PageCount = 100,
                PublishDate = new DateTime(2020, 1, 1),
                Title = "Invalid Genre Id Test"
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void WhenInvalidPageCountGiven_Validator_ShouldBeReturnError(int pageCount)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                GenreId = 1,
                PageCount = pageCount,
                PublishDate = new DateTime(2020, 1, 1),
                Title = "Invalid Page Count Test"
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPublishDateEqualNowGiven_Validator_ShouldBeReturnError()
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                Title = "Invalid Publish Date Test"
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Theory]
        [InlineData(0, 100, "")]
        [InlineData(1, 0, "Title")]
        [InlineData(1, 100, "Ti")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(int genreId, int pageCount, string title)
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = new DateTime(2014, 7, 14),
                Title = title
            };

            // act 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.AddYears(-1),
                Title = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError"
            };

            // act 
            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }

        [Theory]
        [InlineData("")]
        [InlineData("Bo")]
        public void WhenInvalidTitleGiven_Validator_ShouldBeReturnError(string title)
        {
            CreateBookCommand command = new CreateBookCommand(null, null);
            command.model = new CreateBookModel()
            {
                Title = title,
                PageCount = 100,
                PublishDate =new DateTime(2014, 7, 14),
                GenreId = 1
            };

            CreateBookCommandValidator validator = new CreateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}