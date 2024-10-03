using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLChapterImageRepository : IChapterImageRepository
    {
        private readonly MangaNNovelAuthDBContext _context;

        public SQLChapterImageRepository(MangaNNovelAuthDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ChapterImage>> GetChapterImagesAsync(int chapterContentId)
        {
            return await _context.ChapterImages
                .Where(ci => ci.ChapterContentId == chapterContentId)
                .OrderBy(ci => ci.Order)
                .ToListAsync();
        }

        public async Task AddChapterImageAsync(ChapterImage chapterImage)
        {
            _context.ChapterImages.Add(chapterImage);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateChapterImageAsync(ChapterImage chapterImage)
        {
            _context.ChapterImages.Update(chapterImage);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteChapterImageAsync(int chapterImageId)
        {
            var chapterImage = await _context.ChapterImages.FindAsync(chapterImageId);
            if (chapterImage != null)
            {
                _context.ChapterImages.Remove(chapterImage);
                await _context.SaveChangesAsync();
            }
        }
    }
}