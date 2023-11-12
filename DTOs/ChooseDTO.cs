namespace home_libraryAPI.DTOs
{
    public class ChooseDto
    {
        public int Id { get; set; }

        public string? ImagePath { get; set; }

        public string BookTitle { get; set; } = null!;

        public string? BookSummary { get; set; }
    }
}
