using FluentValidation;

namespace BookStore.Applications.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommandValidator:AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.genreId).GreaterThan(0);
            RuleFor(command => command.updateGenre.Name).NotEmpty().MinimumLength(3);
        }
    }
}
