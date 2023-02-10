using BookStore.DBOperations;
using BookStore.Entities;
using System;

namespace BookStore.UnitTests.TestSetup
{
    public static class Books
    {
        public static void AddBooks(this BookStoreDbContext context)
        {
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
        }
    }
}
