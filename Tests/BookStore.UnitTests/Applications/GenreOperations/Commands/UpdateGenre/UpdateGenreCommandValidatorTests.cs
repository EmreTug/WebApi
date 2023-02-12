using BookStore.Applications.BookOperations.Commands.UpdateBook;
using BookStore.Applications.GenreOperations.Command.UpdateGenre;
using FluentAssertions;
using System;
using Xunit;
using static BookStore.Applications.BookOperations.Commands.UpdateBook.UpdateBookCommand;
using static BookStore.Applications.GenreOperations.Command.UpdateGenre.UpdateGenreCommand;

namespace BookStore.UnitTests.Applications.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData(0, "Name")]
        [InlineData(1, "Na")]
        [InlineData(2, "")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldReturnError(int genreId, string name)
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.genreId = genreId;
            command.updateGenre = new UpdateGenreModel()
            {
                Name = name
            };

            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotReturnError()
        {
            UpdateGenreCommand command = new UpdateGenreCommand(null);
            command.genreId = 1;
            command.updateGenre = new UpdateGenreModel()
            {
                Name = "WhenValidInputsAreGiven_Validator_ShouldNotReturnError",
               
            };
            UpdateGenreCommandValidator validator = new UpdateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }
    }
}

