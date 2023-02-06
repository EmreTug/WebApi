using FluentValidation;

namespace BookStore.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator:AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(query=>query.bookId).GreaterThan(0);
        }
    }
}
