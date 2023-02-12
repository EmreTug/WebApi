using BookStore.Applications.GenreOperations.Command.CreateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore.Applications.GenreOperations.Command.CreateGenre.CreateGenreCommand;

namespace BookStore.UnitTests.Applications.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests
    {
        [Theory]
        [InlineData("")]
        [InlineData("Na")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string Name)
        {
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.model = new CreateGenreModel()
            {
                Name = Name,
            };

            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

     

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateGenreCommand command = new CreateGenreCommand(null, null);
            command.model = new CreateGenreModel()
            {
                Name = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError",
            
            };

            // act 
            CreateGenreCommandValidator validator = new CreateGenreCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
