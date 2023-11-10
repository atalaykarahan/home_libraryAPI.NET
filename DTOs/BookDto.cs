namespace home_libraryAPI.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }
        public string BookTitle { get; set; } = null!;
        public int PublisherId { get; set; }
        public string PublisherName { get; set; } = null!;
        public int AuthorId { get; set; }
        public string AuthorName { get; set; } = null!;
        public string? ImagePath { get; set; }
        public string? BookSummary { get; set; }
        public virtual ICollection<Categories>? Categories { get; set; } = new List<Categories>();

    }

    public class Categories
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}
