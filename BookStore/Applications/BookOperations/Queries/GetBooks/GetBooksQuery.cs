using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.Applications.BookOperations.Queries.GetBooks
{
    public class GetBooksQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBooksQuery(IBookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public List<BooksViewModel> Handle()
        {
           return _context.Books.OrderBy(book => book.Id).ToList().Select(a=> _mapper.Map<BooksViewModel>(a)).ToList();
        }

        public class BooksViewModel
        {
            public string Title { get; set; }

            public string PublishDate { get; set; }
            public int PageCount { get; set; }
            public string Genre { get; set; }

        }
    }
}
