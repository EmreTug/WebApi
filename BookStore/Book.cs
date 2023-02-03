namespace BookStore
{
    public class Book
    {
        public int Id { get; set; }
        public String Title { get; set; }
        public int GenreId { get; set; }
        public int PageCount { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
