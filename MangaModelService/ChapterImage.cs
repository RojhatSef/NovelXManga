namespace MangaModelService
{
    public class ChapterImage
    {
        public int ChapterImageId { get; set; }
        public int ChapterContentId { get; set; }
        public ChapterContent ChapterContent { get; set; }
        public string ImagePath { get; set; }
        public int Order { get; set; } // To maintain the sequence of images
    }
}