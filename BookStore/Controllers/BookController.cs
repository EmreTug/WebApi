using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private static List<Book> BookList = new List<Book>()
        {
            new Book{Id = 1,
                GenreId = 1,
                Title="Lean Startup",//personal grownt
                PageCount=200,
                PublishDate=new DateTime(2001,06,12),
            },
              new Book{Id = 2,
                GenreId = 2,
                Title="Herland",//science fiction
                PageCount=250,
                PublishDate=new DateTime(2010,06,12),
            },
                new Book{Id = 3,
                GenreId = 2,
                Title="Herland",//science fiction
                PageCount=500,
                PublishDate=new DateTime(2020,06,12),
            },
        };


        [HttpGet]
        public List<Book> GetBooks()
        {
            return BookList.OrderBy(book => book.Id).ToList();
        }
        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return BookList.FirstOrDefault(book=>book.Id==id);
        }
        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    return BookList.FirstOrDefault(book => book.Id == Convert.ToInt16(id));
        //}
        [HttpPost]
        public IActionResult AddBook(Book newBook) {
            var result = BookList.Any(a => a == newBook);
            if (result is true)
                return BadRequest();
            BookList.Add(newBook);
            return Ok();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id,[FromBody]Book updateBok) {
            var result = BookList.FirstOrDefault(a => a.Id == id);
            if (result is null)
                return BadRequest();
            result.PublishDate = updateBok.PublishDate!=default?updateBok.PublishDate:result.PublishDate;
            result.GenreId = updateBok.GenreId != default ? updateBok.GenreId : result.GenreId;
            result.Title=updateBok.Title != default ? updateBok.Title : result.Title;
            result.PageCount=updateBok.PageCount != default ? updateBok.PageCount : result.PageCount;
            return Ok();
            
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            var result = BookList.FirstOrDefault(a => a.Id == id);
            if (result is null)
                return BadRequest();
            BookList.Remove(result);
            return Ok();

        }


    }
}