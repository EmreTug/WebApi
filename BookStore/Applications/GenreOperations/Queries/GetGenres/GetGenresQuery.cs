using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.Applications.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenresQuery(BookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<GenresViewModel> Handle()
        {
            return _context.Genres.OrderBy(genre => genre.Id).ToList().Select(a => _mapper.Map<GenresViewModel>(a)).ToList();
        }

        public class GenresViewModel
        {
            public string Name { get; set; }


        }
    }
}
