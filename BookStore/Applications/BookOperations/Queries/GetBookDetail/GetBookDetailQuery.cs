using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.Applications.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public int bookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public BookDetailViewModel Handle()
        {
            var result = _context.Books.FirstOrDefault(book => book.Id == bookId);
            if (result == null)
                throw new InvalidOperationException("Kitap Bulunamadı");
            return _mapper.Map<BookDetailViewModel>(result);
        }

        public class BookDetailViewModel
        {
            public string Title { get; set; }

            public string PublishDate { get; set; }
            public int PageCount { get; set; }
            public string Genre { get; set; }

        }
    }
}
