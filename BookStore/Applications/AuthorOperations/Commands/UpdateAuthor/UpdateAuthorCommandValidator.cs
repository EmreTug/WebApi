using FluentValidation;

namespace BookStore.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.authorId).GreaterThan(0);
            RuleFor(command => command.updateAuthor.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.updateAuthor.Name).NotEmpty().MinimumLength(3);
            RuleFor(command => command.updateAuthor.Surname).NotEmpty().MinimumLength(3);
        }
    }
}
