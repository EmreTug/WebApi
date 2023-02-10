using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Applications.GenreOperations.Command.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var result = _context.Books.Any(a => a.Title == model.Name);
            if (result is true)
                throw new InvalidOperationException("Genre kayıtlı");
            _context.Genres.Add(_mapper.Map<Genre>(model));
            _context.SaveChanges();
        }

        public class CreateGenreModel
        {
            public string Name { get; set; }
   

        }
    }
}
