using BookStore.DBOperations;
using BookStore.Entities;

namespace BookStore.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(new Genre { Name = "Personal Growth" }, new Genre { Name = "Sciene Fiction" }, new Genre { Name = "Romance" });
          
        }
    }
}
