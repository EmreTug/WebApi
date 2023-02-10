using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int authorId { get; set; }
        public GetAuthorDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public AuthorDetailViewModel Handle()
        {
            var result = _context.Books.FirstOrDefault(author => author.Id == authorId);
            if (result == null)
                throw new InvalidOperationException("Author");
            return _mapper.Map<AuthorDetailViewModel>(result);
        }

        public class AuthorDetailViewModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }

        }
    }
}
