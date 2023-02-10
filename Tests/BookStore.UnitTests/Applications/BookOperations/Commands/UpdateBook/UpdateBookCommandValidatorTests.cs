using BookStore.Applications.BookOperations.Commands.UpdateBook;
using FluentAssertions;
using System;
using Xunit;
using static BookStore.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;

namespace BookStore.UnitTests.Applications.BookOperations.Command.UpdateBook
{
    public class UpdateBookCommandValidatorTests
    {
        [Theory]
        [InlineData(0, "Title", 0, 0)]
        [InlineData(1, "", 1, 0)]
        [InlineData(2, "Ti", 1, 0)]
        [InlineData(3, "Title", 1, -1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int bookId, string title, int genreId, int pageCount)
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.bookId = bookId;
            command.updateBok = new UpdateBookModel()
            {
                Title = title,
                GenreId = genreId,
                PageCount = pageCount,
                PublishDate = new DateTime(2014, 7, 14)
            };

            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenPublishDateEqualNowGiven_Validator_ShouldReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.bookId = 1;
            command.updateBok = new UpdateBookModel()
            {
                Title = "WhenPublishDateEqualNowGiven_Validator_ShouldReturnError",
                GenreId = 1,
                PageCount = 100,
                PublishDate = DateTime.Now.Date
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateBookCommand command = new UpdateBookCommand(null);
            command.bookId = 1;
            command.updateBok = new UpdateBookModel()
            {
                Title = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
                GenreId = 1,
                PageCount = 100,
                PublishDate = new DateTime(2014, 7, 14)
            };
            UpdateBookCommandValidator validator = new UpdateBookCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

