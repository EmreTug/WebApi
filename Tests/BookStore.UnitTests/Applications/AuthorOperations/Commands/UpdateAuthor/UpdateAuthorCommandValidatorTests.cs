using BookStore.Applications.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using BookStore.UnitTests.TestSetup;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using static BookStore.Applications.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace BookStore.UnitTests.Applications.AuthorOperations.Command.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests: IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;


        public UpdateAuthorCommandValidatorTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }
        [Theory]
        [InlineData(0,"Name", "")]
        [InlineData(1,"", "Surname")]
        [InlineData(2,"Name", "Su")]
        [InlineData(3,"Na", "Su")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnError(int authorId, string name, string surname)
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.authorId = authorId;
            command.updateAuthor = new UpdateAuthorModel()
            {
                Name = name,
                Surname = surname,
                DateOfBirth = new DateTime(2014, 7, 14)
            };

            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.authorId = 1;
            command.updateAuthor = new UpdateAuthorModel()
            {
                Name = "WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError",
                Surname = "WhenDateTimeEqualNowGiven_Validator_ShouldBeReturnError",
                DateOfBirth = DateTime.Now.Date
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().BeGreaterThan(0);
        }
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnError()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(null);
            command.authorId = 1;
            command.updateAuthor = new UpdateAuthorModel()
            {
                Name = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError",
                Surname = "WhenValidInputAreGiven_Validator_ShouldNotBeReturnError",
                DateOfBirth = new DateTime(2014, 7, 14)
            };
            UpdateAuthorCommandValidator validator = new UpdateAuthorCommandValidator();
            var result = validator.Validate(command);

            result.Errors.Count.Should().Be(0);
        }

    }
}
