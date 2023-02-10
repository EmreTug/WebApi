using BookStore.DBOperations;

namespace BookStore.Applications.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommand
    {
        private readonly IBookStoreDbContext _context;
        public UpdateBookModel updateBok = new UpdateBookModel();
        public int bookId { get; set; }
        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Books.FirstOrDefault(a => a.Id == bookId);
            if (result is null)
                throw new InvalidOperationException("Kitap bulunamadı");
            result.PublishDate = updateBok.PublishDate != default ? updateBok.PublishDate : result.PublishDate;
            result.GenreId = updateBok.GenreId != default ? updateBok.GenreId : result.GenreId;
            result.Title = updateBok.Title != default ? updateBok.Title : result.Title;
            result.PageCount = updateBok.PageCount != default ? updateBok.PageCount : result.PageCount;
            _context.SaveChanges();
           
        }
        public class UpdateBookModel
        {
           
                public string Title { get; set; }

                public DateTime PublishDate { get; set; }
                public int PageCount { get; set; }
                public int GenreId { get; set; }

            
        }
    }
}
