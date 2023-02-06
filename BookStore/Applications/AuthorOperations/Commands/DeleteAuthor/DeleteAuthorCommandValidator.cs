using FluentValidation;

namespace BookStore.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
