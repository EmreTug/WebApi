using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Applications.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorsQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorsQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<AuthorsViewModel> Handle()
        {
            return _context.Authors.OrderBy(author => author.Id).ToList().Select(a => _mapper.Map<AuthorsViewModel>(a)).ToList();
        }

        public class AuthorsViewModel
        {
            public string Name { get; set; }


        }
    }
}
