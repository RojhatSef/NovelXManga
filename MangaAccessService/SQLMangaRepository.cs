using MangaAccessService.DTO;
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

        // CHANGE ALL TO ASYNC
        public MangaModel GetManga(int id)
        {
            var CurrentManga = mangaNNovelAuthDBContext.mangaModels.Find(id);

            return CurrentManga;
        }

        // CHANGE ALL TO ASYNC
        public MangaModel GetOneMangaAllIncluded(int id)
        {
            var mangaModel = mangaNNovelAuthDBContext.mangaModels.Include(e => e.AllLanguages).Include(e => e.OfficalWebsites)
                .Include(e => e.VoiceActors).Include(e => e.ArtistModels).Include(e => e.Authormodels)
                .Include(e => e.TagsModels).Include(e => e.StudioModels)
                .Include(e => e.reviews).ThenInclude(r => r.UserModels).Include(e => e.Characters)
                .Include(e => e.GenresModels).Include(e => e.BuyPages).ThenInclude(b => b._Languages)
                .Include(e => e.AssociatedNames).Include(e => e.StudioModels)
                .Include(e => e.GroupScanlating).Include(e => e.userModels).Include(e => e.relatedSeries).Include(e => e.RecommendedMangaModels).Include(e => e.BlogModel).FirstOrDefault(e => e.MangaID == id);
            return mangaModel;
        }

        public async Task<MangaModel> GetOneEssentialMangaIncludedAsync(int id)
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .Include(e => e.AllLanguages)
                .Include(e => e.OfficalWebsites)

                .Include(e => e.StudioModels)
                .Include(e => e.reviews.Take(5)).ThenInclude(r => r.UserModels)

                .Include(e => e.BuyPages).ThenInclude(b => b._Languages)
                .Include(e => e.AssociatedNames)
                .Include(e => e.GroupScanlating)
                .Include(e => e.userModels)
                .Include(e => e.relatedSeries)
                .Include(e => e.RecommendedMangaModels)
                .Include(e => e.BlogModel)
                .FirstOrDefaultAsync(e => e.MangaID == id);
        }

        public async Task<MangaModel> GetLimitedEssentialMangaIncludedAsync(int id)
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .Include(e => e.AllLanguages)
                .Include(e => e.OfficalWebsites)

                .Include(e => e.StudioModels)
                .Include(e => e.reviews.Take(5)).ThenInclude(r => r.UserModels)
                .Include(e => e.BuyPages).ThenInclude(b => b._Languages)
                .Include(e => e.AssociatedNames)
                .Include(e => e.GroupScanlating)
                .Include(e => e.userModels)
                .Include(e => e.relatedSeries)
                .Include(e => e.RecommendedMangaModels)
                .Include(e => e.BlogModel)
                .FirstOrDefaultAsync(e => e.MangaID == id);
        }

        public async Task<CurrentMangaDto> GetOneEssentialMangaDtoIncludedAsync(int id)
        {
            var query = mangaNNovelAuthDBContext.mangaModels

                        .Include(e => e.VoiceActors.Take(5))
                        .Include(e => e.ArtistModels.Take(5))
                        .Include(e => e.Authormodels.Take(5))
                        .Include(e => e.TagsModels)
                        .Include(e => e.Characters.Take(5))
                        .Include(e => e.GenresModels)
                        .Where(m => m.MangaID == id);

            return await query.Select(m => new CurrentMangaDto
            {
                MangaID = m.MangaID,

                Artists = m.ArtistModels.Select(a => new CurrentMangaArtistDto
                {
                    ArtistId = a.ArtistId,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath
                }).Take(5).ToList(),

                Authors = m.Authormodels.Select(a => new CurrentMangaAuthorDto
                {
                    AuthorID = a.AuthorID,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath
                }).Take(5).ToList(),

                Characters = m.Characters.Select(c => new CurrentMangaCharacterDto
                {
                    CharacterId = c.CharacterId,
                    CharacterName = c.CharacterName,
                    PhotoPath = c.PhotoPath
                }).Take(5).ToList(),

                VoiceActors = m.VoiceActors.Select(v => new CurrentMangaVoiceActorDto
                {
                    VoiceActorId = v.VoiceActorId,
                    FirstName = v.FirstName,
                    PhotoPath = v.PhotoPath
                }).Take(5).ToList(),

                Genres = m.GenresModels.Select(g => new CurrentMangaGenreDto
                {
                    GenresId = g.GenresId,
                    GenreName = g.GenreName,
                    TagHeavy = g.TagHeavy
                }).ToList(),

                Tags = m.TagsModels.Select(t => new CurrentMangaTagDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                    TagHeavy = t.TagHeavy
                }).ToList()
            }).FirstOrDefaultAsync();
        }

        public async Task<CurrentMangaDto> GetAddtionalEssentialMangaDtoIncludedAsync(int id)
        {
            var query = mangaNNovelAuthDBContext.mangaModels

                        .Include(e => e.VoiceActors)
                        .Include(e => e.ArtistModels)
                        .Include(e => e.Authormodels)
                        .Include(e => e.TagsModels)
                        .Include(e => e.Characters)
                        .Include(e => e.GenresModels)
                        .Where(m => m.MangaID == id);

            return await query.Select(m => new CurrentMangaDto
            {
                MangaID = m.MangaID,

                Artists = m.ArtistModels.Select(a => new CurrentMangaArtistDto
                {
                    ArtistId = a.ArtistId,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath
                }).Take(5).ToList(),

                Authors = m.Authormodels.Select(a => new CurrentMangaAuthorDto
                {
                    AuthorID = a.AuthorID,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath
                }).ToList(),

                Characters = m.Characters.Select(c => new CurrentMangaCharacterDto
                {
                    CharacterId = c.CharacterId,
                    CharacterName = c.CharacterName,
                    PhotoPath = c.PhotoPath
                }).ToList(),

                VoiceActors = m.VoiceActors.Select(v => new CurrentMangaVoiceActorDto
                {
                    VoiceActorId = v.VoiceActorId,
                    FirstName = v.FirstName,
                    PhotoPath = v.PhotoPath
                }).ToList(),

                Genres = m.GenresModels.Select(g => new CurrentMangaGenreDto
                {
                    GenresId = g.GenresId,
                    GenreName = g.GenreName,
                    TagHeavy = g.TagHeavy
                }).ToList(),

                Tags = m.TagsModels.Select(t => new CurrentMangaTagDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                    TagHeavy = t.TagHeavy
                }).ToList()
            }).FirstOrDefaultAsync();
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

        public async Task<MangaModel> AddAsync(MangaModel NewManga)
        {
            await mangaNNovelAuthDBContext.mangaModels.AddAsync(NewManga);
            await mangaNNovelAuthDBContext.SaveChangesAsync();
            return NewManga;
        }

        public async Task<MangaModel> DeleteAsync(int id)
        {
            MangaModel mangaModel = await mangaNNovelAuthDBContext.mangaModels.FindAsync(id);
            if (mangaModel != null)
            {
                mangaNNovelAuthDBContext.mangaModels.Remove(mangaModel);
                await mangaNNovelAuthDBContext.SaveChangesAsync();
            }
            return mangaModel;
        }

        public async Task<IEnumerable<MangaModel>> GetPaginatedMangaModelsAsync(int pageNumber, int pageSize)
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .Include(e => e.GenresModels)
                .Include(e => e.TagsModels)
                .Include(e => e.ArtistModels)
                .Include(e => e.Authormodels)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<IEnumerable<MangaModel>> GetAllModelAsync()
        {
            return await mangaNNovelAuthDBContext.mangaModels.Include(e => e.reviews).ToListAsync();
        }

        public async Task<IEnumerable<MangaModel>> Get10MangaModelAsync()
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .Include(e => e.reviews.Take(3))
                .Include(e => e.Authormodels)
                .Include(e => e.ArtistModels)
                .Include(e => e.GenresModels)
                .Include(e => e.TagsModels)
                .OrderByDescending(m => m.BookAddedToDB) // Assuming you want the latest added books
                .Take(10)
                .ToListAsync();
        }

        public async Task<MangaModel> GetModelAsync(int id)
        {
            var CurrentManga = await mangaNNovelAuthDBContext.mangaModels.FindAsync(id);
            return CurrentManga;
        }

        public async Task<MangaModel> GetOneMangaAllIncludedAsync(int id)
        {
            var mangaModel = await mangaNNovelAuthDBContext.mangaModels.Include(e => e.AllLanguages).Include(e => e.OfficalWebsites)
                .Include(e => e.VoiceActors).Include(e => e.ArtistModels).Include(e => e.Authormodels)
                .Include(e => e.TagsModels).Include(e => e.StudioModels)
                .Include(e => e.reviews).ThenInclude(r => r.UserModels).Include(e => e.Characters)
                .Include(e => e.GenresModels).Include(e => e.BuyPages).ThenInclude(b => b._Languages)
                .Include(e => e.AssociatedNames).Include(e => e.StudioModels)
                .Include(e => e.GroupScanlating).Include(e => e.userModels).Include(e => e.relatedSeries).Include(e => e.RecommendedMangaModels).Include(e => e.BlogModel).FirstOrDefaultAsync(e => e.MangaID == id);
            return mangaModel;
        }

        public async Task<IEnumerable<MangaModel>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return await mangaNNovelAuthDBContext.mangaModels.ToListAsync();
            }
            return await mangaNNovelAuthDBContext.mangaModels.Where(s => s.MangaName.Contains(searchTerm) || s.ISBN13.Contains(searchTerm) || s.ISBN10.Contains(searchTerm)).ToListAsync();
        }

        public async Task<MangaModel> UpdateAsync(MangaModel updatedManga)
        {
            var manga = mangaNNovelAuthDBContext.mangaModels.Attach(updatedManga);
            manga.State = EntityState.Modified;
            await mangaNNovelAuthDBContext.SaveChangesAsync();
            return updatedManga;
        }

        public async Task<IEnumerable<MangaModel>> GetTopRankedMangaAsync(int count)
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .OrderByDescending(manga => manga.OverAllBookScore)
                .Take(count)
                .ToListAsync();
        }

        public async Task<IEnumerable<MangaModel>> GetMostPopularMangaAsync(int count)
        {
            return await mangaNNovelAuthDBContext.mangaModels
                .OrderByDescending(manga => manga.DailyRead + manga.WeekRead + manga.MonthRead + manga.YearRead + manga.ForeverRead)
                .Take(count)
                .ToListAsync();
        }

        public async Task UpdateMangaRankingAsync(MangaModel manga, int rank)
        {
            manga.Rank = rank;
            mangaNNovelAuthDBContext.mangaModels.Update(manga);
            await mangaNNovelAuthDBContext.SaveChangesAsync();
        }
    }
}