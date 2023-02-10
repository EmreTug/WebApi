using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.Applications.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommand
    {
        public CreateAuthorModel model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void Handle()
        {
            var result = _context.Authors.Any(a => a.Name == model.Name&&a.Surname==model.Surname);
            if (result is true)
                throw new InvalidOperationException("Author kayıtlı");
            _context.Authors.Add(_mapper.Map<Author>(model));
            _context.SaveChanges();
        }

        public class CreateAuthorModel
        {
            public string Name { get; set; }
            public string Surname { get; set; }
            public DateTime DateOfBirth { get; set; }

        }
    }
}
