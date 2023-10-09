namespace home_libraryAPI.DTOs
{
    public class BookDto
    {
        public int Id { get; set; }

        public string BookTitle { get; set; } = null!;

        public string PublisherName { get; set; } = null!;

        public virtual ICollection<Categories> Categories { get; set; } = new List<Categories>();

    }

    public class Categories
    {
        public int Id { get; set; }

        public string CategoryName { get; set; } = null!;
    }
}
