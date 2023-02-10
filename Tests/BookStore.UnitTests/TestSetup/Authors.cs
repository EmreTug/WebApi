using BookStore.DBOperations;
using BookStore.Entities;
using System;

namespace BookStore.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
            context.Authors.AddRange(
                new Author { DateOfBirth = new DateTime(2001, 06, 12), Name = "AuthorName1", Surname = "AuthorSurname1" },
                new Author { DateOfBirth = new DateTime(2001, 06, 11), Name = "AuthorName2", Surname = "AuthorSurname2" },
                new Author { DateOfBirth = new DateTime(2002, 06, 11), Name = "AuthorName3", Surname = "AuthorSurname3" }
                );

        }
    }
}
