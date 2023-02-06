using FluentValidation;

namespace BookStore.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator:AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.bookId).GreaterThan(0);
            RuleFor(command => command.updateBok.PublishDate).NotEmpty().LessThan(DateTime.Now.Date);
            RuleFor(command => command.updateBok.Title).NotEmpty().MinimumLength(3);
            RuleFor(command => command.updateBok.GenreId).GreaterThan(0);
            RuleFor(command => command.updateBok.PageCount).GreaterThan(0);



        }
    }
}
