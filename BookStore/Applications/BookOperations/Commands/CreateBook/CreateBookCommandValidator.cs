using FluentValidation;

namespace BookStore.Applications.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidator:AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(command => command.model.GenreId).GreaterThan(0);
            RuleFor(command => command.model.PageCount).GreaterThan(0);
            RuleFor(command => command.model.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.model.Title).NotEmpty().MinimumLength(3);

        }
    }
}
