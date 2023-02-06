using BookStore.Entities;
using Microsoft.EntityFrameworkCore;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Genres.AddRange(new Genre { Name="Personal Growth"},new Genre { Name="Sciene Fiction"},new Genre { Name="Romance"});
                context.Books.AddRange(new Book
                {
                   // Id = 1,
                    GenreId = 1,
                    Title = "Lean Startup",
                    PageCount = 200,
                    PublishDate = new DateTime(2001, 06, 12),
                },
              new Book
              {
                  //Id = 2,
                  GenreId = 2,
                  Title = "Herland",
                  PageCount = 250,
                  PublishDate = new DateTime(2010, 06, 12),
              },
                new Book
                {
                    //Id = 3,
                    GenreId = 2,
                    Title = "Herland",
                    PageCount = 500,
                    PublishDate = new DateTime(2020, 06, 12),
                });
                context.SaveChanges();
            }
        }
    }
}
