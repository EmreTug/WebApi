using AutoMapper;
using BookStore.Entities;
using static BookStore.Applications.AuthorOperations.Commands.CreateAuthor.CreateAuthorCommand;
using static BookStore.Applications.AuthorOperations.Queries.GetAuthorDetail.GetAuthorDetailQuery;
using static BookStore.Applications.AuthorOperations.Queries.GetAuthors.GetAuthorsQuery;
using static BookStore.Applications.BookOperations.Commands.CreateBook.CreateBookCommand;
using static BookStore.Applications.BookOperations.Queries.GetBookDetail.GetBookDetailQuery;
using static BookStore.Applications.BookOperations.Queries.GetBooks.GetBooksQuery;
using static BookStore.Applications.GenreOperations.Command.CreateGenre.CreateGenreCommand;
using static BookStore.Applications.GenreOperations.Queries.GetGenreDetail.GetGenreDetailQuery;
using static BookStore.Applications.GenreOperations.Queries.GetGenres.GetGenresQuery;

namespace BookStore.Common
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateBookModel, Book>();
            CreateMap<Book, BooksViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ( (GenreEnum)src.GenreId).ToString()));
            CreateMap<Book, BookDetailViewModel>().ForMember(dest => dest.Genre, opt => opt.MapFrom(src => ( (GenreEnum)src.GenreId).ToString()));
            CreateMap<Genre, GenresViewModel>();
            CreateMap<Genre, GenreDetailViewModel>();
            CreateMap<CreateGenreModel, Genre>();
            CreateMap<Author, AuthorsViewModel>();
            CreateMap<Author, AuthorDetailViewModel>();
            CreateMap<CreateAuthorModel, Author>();



        }
    }
}
