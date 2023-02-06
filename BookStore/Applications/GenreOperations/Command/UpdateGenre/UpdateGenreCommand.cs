using BookStore.DBOperations;

namespace BookStore.Applications.GenreOperations.Command.UpdateGenre
{
    public class UpdateGenreCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateGenreModel updateGenre = new UpdateGenreModel();
        public int genreId { get; set; }
        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Genres.FirstOrDefault(a => a.Id == genreId);
            if (result is null)
                throw new InvalidOperationException("Genre bulunamadı");
            result.Name = updateGenre.Name != default ? updateGenre.Name : result.Name;
       
            _context.SaveChanges();

        }
        public class UpdateGenreModel
        {

            public string Name { get; set; }



        }
    }
}
