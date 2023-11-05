namespace home_libraryAPI.DTOs
{
    public class ChooseDTO
    {
        public int Id { get; set; }

        public string? ImagePath { get; set; }

        public string BookTitle { get; set; } = null!;

        public string? BookSummary { get; set; }
    }
}
