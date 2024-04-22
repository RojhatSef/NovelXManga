namespace NovelXManga.Pages.SearchFilter
{
    public class SearchResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public string Type { get; set; }
        public string? CompletelyTranslated { get; set; }
        public int? Rank { get; set; }
    }
}