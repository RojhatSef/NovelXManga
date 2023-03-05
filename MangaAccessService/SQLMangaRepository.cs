using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService.Migrations
{
    public class SQLMangaRepository : IMangaRepository
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;

        public SQLMangaRepository(MangaNNovelAuthDBContext mangaNNovelAuthDBContext)
        {
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
        }

        public MangaModel Add(MangaModel NewManga)
        {
            mangaNNovelAuthDBContext.mangaModels.Add(NewManga);
            mangaNNovelAuthDBContext.SaveChanges();
            return NewManga;
        }

        public MangaModel Delete(int id)
        {
            MangaModel mangaModel = mangaNNovelAuthDBContext.mangaModels.Find(id);
            if (mangaModel != null)
            {
                mangaNNovelAuthDBContext.mangaModels.Remove(mangaModel);
                mangaNNovelAuthDBContext.SaveChanges();
            }
            return mangaModel;
        }

        public IEnumerable<MangaModel> GetAllManga()
        {
            return mangaNNovelAuthDBContext.mangaModels
                //.Include(e => e.BlogModel)
                .Include(s => s.GroupScanlating)
                .Include(b => b.BlogModel.postsModel)
                .Include(x => x.RecommendedMangaModels)
                .Include(e => e.relatedSeries)
                .Include(f => f.OfficalWebsites);
        }

        public MangaModel GetManga(int id)
        {
            var CurrentManga = mangaNNovelAuthDBContext.mangaModels.Find(id);

            return CurrentManga;
        }

        public MangaModel GetOneMangaAllIncluded(int id)
        {
            var mangaModel = mangaNNovelAuthDBContext.mangaModels.Include(e => e.AllLanguages).Include(e => e.OfficalWebsites)
                .Include(e => e.VoiceActors).Include(e => e.ArtistModels).Include(e => e.Authormodels)
                .Include(e => e.TagsModels).Include(e => e.StudioModels)
                .Include(e => e.reviews).Include(e => e.Characters)
                .Include(e => e.GenresModels).Include(e => e.BuyPages)
                .Include(e => e.AssociatedNames).Include(e => e.StudioModels)
                .Include(e => e.GroupScanlating).Include(e => e.userModels).Include(e => e.relatedSeries).Include(e => e.RecommendedMangaModels).Include(e => e.BlogModel).FirstOrDefault(e => e.MangaID == id);
            return mangaModel;
        }

        public IEnumerable<MangaModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return mangaNNovelAuthDBContext.mangaModels;
            }
            return mangaNNovelAuthDBContext.mangaModels.Where(s => s.MangaName.Contains(searchTerm) || s.ISBN13.Contains(searchTerm) || s.ISBN10.Contains(searchTerm));
        }

        public MangaModel Update(MangaModel updatedManga)
        {
            var manga = mangaNNovelAuthDBContext.mangaModels.Attach(updatedManga);
            manga.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            mangaNNovelAuthDBContext.SaveChanges();
            return updatedManga;
        }
    }
}