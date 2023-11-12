namespace home_libraryAPI.DTOs
{
    public class HomePageFeatureDto
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public int BookCount { get; set; }
        public virtual ICollection<BookDtos> Books { get; set; } = new List<BookDtos>();

    }

    public class BookDtos
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? ImagePath { get; set; }
        public string? BookSummary { get; set; }
    }
}
