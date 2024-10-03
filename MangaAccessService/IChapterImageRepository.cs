using MangaModelService;

namespace MangaAccessService
{
    public interface IChapterImageRepository
    {
        Task<IEnumerable<ChapterImage>> GetChapterImagesAsync(int chapterContentId);

        Task AddChapterImageAsync(ChapterImage chapterImage);

        Task UpdateChapterImageAsync(ChapterImage chapterImage);

        Task DeleteChapterImageAsync(int chapterImageId);
    }
}