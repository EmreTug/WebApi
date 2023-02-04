using BookStore.Common;
using BookStore.DBOperations;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        private readonly BookStoreDbContext _context;
        public int bookId { get; set; }
        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }
        public BookDetailViewModel Handle()
        {
            var result = _context.Books.FirstOrDefault(book => book.Id == bookId);
            if (result == null)
                throw new InvalidOperationException("Kitap Bulunamadı");
            return new BookDetailViewModel { Genre=((GenreEnum)result.GenreId).ToString(),PageCount=result.PageCount,PublishDate=result.PublishDate.ToString("dd/MM,yyyy"),Title=result.Title };
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
