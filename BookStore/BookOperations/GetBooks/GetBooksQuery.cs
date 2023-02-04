using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBooks
{
    public class GetBooksQuery
    {
        private readonly BookStoreDbContext _context;
        public GetBooksQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public List<BooksViewModel> Handle()
        {
           return _context.Books.OrderBy(book => book.Id).ToList().Select(a=>new BooksViewModel { Genre=((GenreEnum)a.GenreId).ToString(), PageCount=a.PageCount,PublishDate=a.PublishDate.ToString("dd/MM/yyyy"),Title=a.Title}).ToList();
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
