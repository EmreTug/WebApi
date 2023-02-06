using BookStore.DBOperations;

namespace BookStore.Applications.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommand
    {
        private readonly BookStoreDbContext _context;
        public int Id { get; set; }
        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Books.FirstOrDefault(a => a.Id == Id);
            if (result is null)
                throw new InvalidOperationException("Kitap Bulunamadı");
            _context.Books.Remove(result);
            _context.SaveChanges();
        }

    }
}
