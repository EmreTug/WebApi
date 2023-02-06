using FluentValidation;

namespace BookStore.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator:AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command=>command.model.Name).NotEmpty().MinimumLength(3);
            RuleFor(command=>command.model.Surname).NotEmpty().MinimumLength(3);
            RuleFor(command=>command.model.DateOfBirth).NotEmpty().LessThan(DateTime.Now.Date);
        }
    }
}
