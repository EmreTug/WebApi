using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Applications.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int genreId { get; set; }
        public GetGenreDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public GenreDetailViewModel Handle()
        {
            var result = _context.Books.FirstOrDefault(book => book.Id == genreId);
            if (result == null)
                throw new InvalidOperationException("Genre");
            return _mapper.Map<GenreDetailViewModel>(result);
        }

        public class GenreDetailViewModel
        {
            public string Name { get; set; }
            public bool IsActive { get; set; }

        }
    }
}
