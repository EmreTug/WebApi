using BookStore.DBOperations;

namespace BookStore.Applications.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        private readonly BookStoreDbContext _context;
        public UpdateAuthorModel updateAuthor = new UpdateAuthorModel();
        public int authorId { get; set; }
        public UpdateAuthorCommand(BookStoreDbContext context)
        {
            _context = context;
        }
        public void Handle()
        {
            var result = _context.Authors.FirstOrDefault(a => a.Id == authorId);
            if (result is null)
                throw new InvalidOperationException("Author bulunamadı");
            result.Name = updateAuthor.Name != default ? updateAuthor.Name : result.Name;
            result.Surname = updateAuthor.Surname != default ? updateAuthor.Surname : result.Surname;
            result.DateOfBirth = updateAuthor.DateOfBirth != default ? updateAuthor.DateOfBirth : result.DateOfBirth;
            _context.SaveChanges();

        }
        public class UpdateAuthorModel
        {

            public string Name { get; set; }
            public string Surname { get; set; }

            public DateTime DateOfBirth { get; set; }
        


        }
    }
}
