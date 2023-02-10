using BookStore.DBOperations;

namespace BookStore.Applications.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        private readonly IBookStoreDbContext _context;
        public int Id { get; set; }
        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Authors.FirstOrDefault(a => a.Id == Id);
            if (result is null)
                throw new InvalidOperationException("Author Bulunamadı");
            if (_context.Books.FirstOrDefault(a=>a.AuthorId==result.Id)!=null)
                throw new InvalidOperationException("Kullanıcı silinemez kayıtlı kitap var");
            _context.Authors.Remove(result);
            _context.SaveChanges();
        }
    }
}
