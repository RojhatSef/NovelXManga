using MangaModelService;

namespace MangaAccessService
{
    public interface IChapterContentRepository
    {
        Task<ChapterContent> GetChapterContentAsync(int chapterId);

        Task AddChapterContentAsync(ChapterContent chapterContent);

        Task UpdateChapterContentAsync(ChapterContent chapterContent);

        Task DeleteChapterContentAsync(int chapterContentId);
    }
}