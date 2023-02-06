using FluentValidation;

namespace BookStore.Applications.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommandValidator:AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.model.Name).NotEmpty().MinimumLength(3);
        }
    }
}
