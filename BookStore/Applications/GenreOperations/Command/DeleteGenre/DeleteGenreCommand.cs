using BookStore.DBOperations;

namespace BookStore.Applications.GenreOperations.Command.DeleteGenre
{
    public class DeleteGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public int Id { get; set; }
        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Genres.FirstOrDefault(a => a.Id == Id);
            if (result is null)
                throw new InvalidOperationException("Genre Bulunamadı");
            _context.Genres.Remove(result);
            _context.SaveChanges();
        }
    }
}
