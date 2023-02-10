using FluentAssertions;
using System;
using System.Linq;
using BookStore.DBOperations;
using BookStore.Applications.AuthorOperations.Commands.UpdateAuthor;
using Moq;
using Xunit;
using BookStore.Entities;
using System.Collections.Generic;
using BookStore.UnitTests.TestSetup;
using static BookStore.Applications.AuthorOperations.Commands.UpdateAuthor.UpdateAuthorCommand;

namespace BookStore.UnitTests.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;


        public UpdateAuthorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
        }

        [Fact]
        public void WhenNotExistAuthorIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.authorId = 555;
            FluentActions
                .Invoking(() => command.Handle())
                .Should().Throw<InvalidOperationException>().And.Message.Should().Be("Author bulunamadı");
        }
        [Fact]

        public void WhenExistAuthorIdAndUpdateAuthorModelAreGiven_Author_ShouldBeUpdated()
        {
            var author = new Author()
            {
                Name = "WhenExistAuthorIdAndUpdateAuthorModelAreGiven_Author_ShouldBeUpdated",
                Surname = "WhenExistAuthorIdAndUpdateAuthorModelAreGiven_Author_ShouldBeUpdated",
                DateOfBirth = DateTime.Now.AddYears(-2),
            };
            _context.Authors.Add(author);
            _context.SaveChanges();


            var authorId = _context.Authors.SingleOrDefault(x => x.Name == author.Name && x.Surname == author.Surname).Id;

            var updateModel = new UpdateAuthorModel()
            {
                Name = "Updated",
                Surname ="Updated",
                DateOfBirth = author.DateOfBirth.AddYears(-1),
            };
            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.authorId = authorId;
            command.updateAuthor = updateModel;
            FluentActions
                .Invoking(() => command.Handle()).Invoke();

            _context.Authors.Any(x => x.Id == authorId
                            && x.Name == updateModel.Name
                            && x.Surname == updateModel.Surname
                            && x.DateOfBirth == updateModel.DateOfBirth
            ).Should().BeTrue();
        }
        

      

    }
}
