using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLChapterContentRepository : IChapterContentRepository
    {
        private readonly MangaNNovelAuthDBContext _context;

        public SQLChapterContentRepository(MangaNNovelAuthDBContext context)
        {
            _context = context;
        }

        public async Task<ChapterContent> GetChapterContentAsync(int chapterId)
        {
            return await _context.ChapterContents
                .Include(cc => cc.Images)
                .FirstOrDefaultAsync(cc => cc.ChapterId == chapterId);
        }

        public async Task AddChapterContentAsync(ChapterContent chapterContent)
        {
            _context.ChapterContents.Add(chapterContent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChapterContentAsync(ChapterContent chapterContent)
        {
            _context.ChapterContents.Update(chapterContent);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterContentAsync(int chapterContentId)
        {
            var chapterContent = await _context.ChapterContents.FindAsync(chapterContentId);
            if (chapterContent != null)
            {
                _context.ChapterContents.Remove(chapterContent);
                await _context.SaveChangesAsync();
            }
        }
    }
}