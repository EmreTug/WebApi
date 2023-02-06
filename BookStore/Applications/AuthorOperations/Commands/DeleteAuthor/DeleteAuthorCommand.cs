using BookStore.DBOperations;

namespace BookStore.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public int Id { get; set; }
        public DeleteAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Authors.FirstOrDefault(a => a.Id == Id);
            if (result is null)
                throw new InvalidOperationException("Author Bulunamadı");
            _context.Authors.Remove(result);
            _context.SaveChanges();
        }
    }
}
