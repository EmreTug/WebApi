using AutoMapper;
using BookStore.DBOperations;

namespace BookStore.BookOperations.CreateBook
{
    public class CreateBookCommand
    {
        public CreateBookModel model { get; set; }
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommand(BookStoreDbContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var result = _context.Books.Any(a => a.Title == model.Title);
            if (result is true)
                throw new InvalidOperationException("Kitap kayıtlı");
            _context.Books.Add(_mapper.Map<Book>(model));
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
