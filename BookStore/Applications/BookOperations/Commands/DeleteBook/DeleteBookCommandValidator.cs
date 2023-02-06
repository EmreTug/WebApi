using FluentValidation;

namespace BookStore.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidator:AbstractValidator<DeleteBookCommand>
    {
        public DeleteBookCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
