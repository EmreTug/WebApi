using FluentValidation;

namespace BookStore.Applications.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommandValidator:AbstractValidator<DeleteGenreCommand>
    {
        public DeleteGenreCommandValidator()
        {
            RuleFor(command => command.Id).GreaterThan(0);
        }
    }
}
