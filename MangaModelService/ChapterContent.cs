namespace MangaModelService
{
    public class ChapterContent
    {
        public int ChapterContentId { get; set; }
        public int ChapterId { get; set; }
        public ChapterModel Chapter { get; set; }

        // For novels
        public string TextContent { get; set; }

        // For manga
        public ICollection<ChapterImage> Images { get; set; }
    }
}