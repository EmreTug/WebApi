using BookStore.DBOperations;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel model { get; set; }
        private readonly BookStoreDbContext _context;
        public CreateBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Books.Any(a => a.Title == model.Title);
            if (result is true)
                throw new InvalidOperationException("Kitap kayıtlı");
            _context.Books.Add(new Book { Title=model.Title,GenreId=model.GenreId,PageCount=model.PageCount,PublishDate=model.PublishDate});
            _context.SaveChanges();
        }

        public class CreateBookModel{
            public String Title { get; set; }
            public int GenreId { get; set; }
            public int PageCount { get; set; }
            public DateTime PublishDate { get; set; }

        }

    }
}
