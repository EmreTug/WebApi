using BookStore.Applications.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;

namespace BookStore.UnitTests.Applications.AuthorOperations.Command.CreateAuthor
{
    public class CreateAuthorCommandValidatorTests
    {
        [Theory]
        [InlineData("Name", "")]
        [InlineData("", "Surname")]
        [InlineData("Na", "Su")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(string Name, string Surname)
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.model = new CreateAuthorModel()
            {
                Name = Name,
                Surname = Surname,
                DateOfBirth = new DateTime(2014, 7, 14)
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError()
        {
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.model = new CreateAuthorModel()
            {
                Name = "WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError",
                Surname = "WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError",
                DateOfBirth = DateTime.Now.Date
            };

            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            // arrange
            CreateAuthorCommand command = new CreateAuthorCommand(null, null);
            command.model = new CreateAuthorModel()
            {
                Name = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError",
                Surname = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError",
                DateOfBirth = DateTime.Now.AddYears(-1)
            };

            // act 
            CreateAuthorCommandValidator validator = new CreateAuthorCommandValidator();
            var result = validator.Validate(command);

            // assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
