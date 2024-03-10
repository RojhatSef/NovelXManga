using MangaAccessService.DTO;
using MangaAccessService.DTO.IndexDto;
using MangaAccessService.DTO.LoginRegiForgetDto;
using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService.Migrations
{
    public class SQLMangaRepository : IMangaRepository
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;

        private string[] adultSensetivContent = {
    "Adult", "Mature", "Ecchi", "Hentai", "Smut","Horror","Psychological",
    "Rape", "Sexual Abuse", "BDSM", "Erotic", "Anal Intercourse",  "Anilingus", "Attempted Murder", "Attempted Rape",
     "Attempted Suicide", "Bathroom Intercourse", "Big Breasts","Blackmail", "Blood and Gore","Bondage","Borderline H",
      "Caught in the Act", "Child Abuse", "Cunnilingus","Double Penetration", "Drunken Intercourse","Dubious Consent",
      "Exhibitionism","Fellatio", "Fetishes","First-Time Intercourse", "Futanari", "Gang Rape","Glasses-Wearing Uke God",
       "Groping", "Group Intercourse", "Handjob","Incest",  "Lust", "Mind Break", "Mind Control", "Murder","Netorare",
        "Netor", "Netori", "Older Seme Younger Uke","Older Uke Younger Seme", "Outdoor Intercourse", "Paizuri", "Titty Fuck", "Panchira",
         "Prostitute", "Prostitution", "Public Sex","Sadist","School Intercourse","Sex Addict", "Sex Friends Become Lovers", "Sex Toy", "Sex", "Sexual Abuse",
           "Straight Seme", "Straight Uke","Suicide", "Threesome", "Undergarment","Urination", "Lolicon", "Shotacon"

}; private string[] MatureSensetivContent = {

    "Adult", "Hentai", "Smut",
    "Rape", "Sexual Abuse",  "Erotic", "Anal Intercourse",  "Anilingus", "Attempted Murder",
     "Bathroom Intercourse", "Big Breasts","Bondage",
      "Caught in the Act",  "Cunnilingus","Double Penetration", "Drunken Intercourse",
      "Exhibitionism","Fellatio", "Fetishes","First-Time Intercourse", "Futanari", "Gang Rape",
       "Groping", "Group Intercourse", "Handjob","Incest",  "Lust", "Mind Break", "Mind Control", "Netorare",
        "Netor", "Netori","Outdoor Intercourse", "Paizuri", "Titty Fuck", "Panchira","Urination",
        "Public Sex","Sadist","School Intercourse","Sex Addict", "Sex Friends Become Lovers", "Sex Toy", "Sex", "Sexual Abuse", "Lolicon", "Shotacon"
};

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
                    LastName = a.LastName
                }).Take(5).ToList(),

                Authors = m.Authormodels.Select(a => new CurrentMangaAuthorDto
                {
                    AuthorID = a.AuthorID,
                    FirstName = a.FirstName,
                    LastName = a.LastName
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
                    LastName = v.LastName
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
                    LastName = a.LastName
                }).Take(5).ToList(),

                Authors = m.Authormodels.Select(a => new CurrentMangaAuthorDto
                {
                    AuthorID = a.AuthorID,
                    FirstName = a.FirstName,
                    LastName = a.LastName
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
                    LastName = v.LastName
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

        public async Task<UserSettingsDTO> GetUserSettingsAsync(string userId)
        {
            var userSettings = await mangaNNovelAuthDBContext.UserSettings
                .Where(us => us.UserModelId == userId)
                .Select(us => new UserSettingsDTO
                {
                    UserModelId = us.UserModelId,
                    ShowAdultContent = us.showAdultContent,
                    ShowMatureContent = us.ShowMatureContent
                })
                .FirstOrDefaultAsync();

            return userSettings ?? new UserSettingsDTO(); // Return empty settings if not found
        }

        public async Task<IEnumerable<MangaModel>> GetAllUserModelAsync(UserModel currentUser)
        {
            IQueryable<MangaModel> query = mangaNNovelAuthDBContext.mangaModels.Include(e => e.reviews);

            // Default settings for guests (or when there's no user)
            UserSettingsDTO defaultSettings = new UserSettingsDTO
            {
                ShowAdultContent = false,
                ShowMatureContent = false
            };

            // Fetch user settings if a user is logged in, otherwise use default settings
            UserSettingsDTO userSettings = currentUser != null
                ? await GetUserSettingsAsync(currentUser.Id)
                : defaultSettings;

            // Apply filters based on user settings or default settings
            query = ApplyMatureContentFilter(query, userSettings.ShowMatureContent);
            query = ApplyAdultContentFilter(query, userSettings.ShowAdultContent);

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<MangaModel>> GetAllModelAsync()
        {
            return await mangaNNovelAuthDBContext.mangaModels.Include(e => e.reviews).ToListAsync();
        }

        public async Task<IEnumerable<MangaModel>> Get10MangaModelAsync()
        {
            return await mangaNNovelAuthDBContext.mangaModels

                .Include(e => e.Authormodels.Take(2))
                .Include(e => e.ArtistModels.Take(2))
                .Include(e => e.GenresModels.Take(5))
                .Include(e => e.TagsModels.Take(5))
                .OrderByDescending(m => m.BookAddedToDB) // Assuming you want the latest added books
                .Take(10)
                .ToListAsync();
        }

        public async Task<IEnumerable<IndexDtoManga>> IndexMangaDtoIncludedAsync(UserModel currentUser)
        {
            IQueryable<MangaModel> baseQuery = mangaNNovelAuthDBContext.mangaModels
                            .Include(e => e.ArtistModels)
                            .Include(e => e.Authormodels)
                            .Include(e => e.TagsModels.Take(5))
                            .Include(e => e.GenresModels.Take(5))
                            .Include(e => e.reviews);

            UserSettingsDTO userSettings = currentUser != null
              ? await GetUserSettingsAsync(currentUser.Id)
              : new UserSettingsDTO { ShowAdultContent = false, ShowMatureContent = false };

            // Apply filters based on user settings or default settings
            IQueryable<MangaModel> filteredQuery = ApplyMatureContentFilter(baseQuery, userSettings.ShowMatureContent);
            filteredQuery = ApplyAdultContentFilter(filteredQuery, userSettings.ShowAdultContent);

            // Apply ordering after the filters
            IOrderedQueryable<MangaModel> orderedQuery = filteredQuery.OrderByDescending(m => m.BookAddedToDB);
            return await orderedQuery.Select(m => new IndexDtoManga
            {
                MangaID = m.MangaID,
                PhotoPath = m.PhotoPath,
                ForeverRead = m.ForeverRead,
                MangaName = m.MangaName,
                Description = m.Description,
                ArtistModels = m.ArtistModels.Select(a => new IndexArtistDto
                {
                    ArtistId = a.ArtistId,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath,
                    LastName = a.LastName
                }).ToList(),
                Authormodels = m.Authormodels.Select(a => new IndexAuthorDto
                {
                    AuthorId = a.AuthorID,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath,
                    LastName = a.LastName
                }).ToList(),
                GenresModels = m.GenresModels.Select(g => new IndexGenreDto
                {
                    GenresId = g.GenresId,
                    GenreName = g.GenreName,
                    TagHeavy = g.TagHeavy
                }).Take(5).ToList(),

                TagsModels = m.TagsModels.Select(t => new IndexTagsDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                    TagHeavy = t.TagHeavy
                }).Take(5).ToList(),
                OverAllBookScore = (m.reviews != null && m.reviews.Any())
                    ? Math.Round(m.reviews.Average(r => (r.StylesScore + r.StoryScore + r.GrammarScore + r.CharactersScore) / 4), 1)
                    : (double?)null
            }).ToListAsync();
        }

        public async Task<IEnumerable<LoginRegiForgetCombineDto>> Get10MangaEssentialMangaDtoIncludedAsync(UserModel currentUser)
        {
            IQueryable<MangaModel> baseQuery = mangaNNovelAuthDBContext.mangaModels
                                 .Include(e => e.ArtistModels.Take(5))
                                 .Include(e => e.Authormodels.Take(5))
                                 .Include(e => e.TagsModels.Take(5))
                                 .Include(e => e.GenresModels.Take(5))
                                 .Include(e => e.reviews);

            // Fetch user settings or set to default if user is null
            UserSettingsDTO userSettings = currentUser != null
              ? await GetUserSettingsAsync(currentUser.Id)
              : new UserSettingsDTO { ShowAdultContent = false, ShowMatureContent = false };

            // Apply filters based on user settings or default settings
            IQueryable<MangaModel> filteredQuery = ApplyMatureContentFilter(baseQuery, userSettings.ShowMatureContent);
            filteredQuery = ApplyAdultContentFilter(filteredQuery, userSettings.ShowAdultContent);

            // Apply ordering and limit the results to 10 after applying filters
            IQueryable<MangaModel> finalQuery = filteredQuery.OrderByDescending(m => m.BookAddedToDB).Take(10);

            return await finalQuery.Select(m => new LoginRegiForgetCombineDto
            {
                MangaID = m.MangaID,
                PhotoPath = m.PhotoPath,
                ArtistModels = m.ArtistModels.Select(a => new LoginRegiForgetArtistDto
                {
                    ArtistId = a.ArtistId,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath,
                    LastName = a.LastName
                }).Take(5).ToList(),
                Authormodels = m.Authormodels.Select(a => new LoginRegiForgetAuthorDto
                {
                    AuthorId = a.AuthorID,
                    FirstName = a.FirstName,
                    PhotoPath = a.PhotoPath,
                    LastName = a.LastName
                }).Take(5).ToList(),
                GenresModels = m.GenresModels.Select(g => new LoginRegiForgetGenresDto
                {
                    GenresId = g.GenresId,
                    GenreName = g.GenreName,
                    TagHeavy = g.TagHeavy
                }).Take(5).ToList(),

                TagsModels = m.TagsModels.Select(t => new LoginRegiForgetTagsDto
                {
                    TagId = t.TagId,
                    TagName = t.TagName,
                    TagHeavy = t.TagHeavy
                }).Take(5).ToList(),
                OverAllBookScore = (m.reviews != null && m.reviews.Any())
                    ? Math.Round(m.reviews.Average(r => (r.StylesScore + r.StoryScore + r.GrammarScore + r.CharactersScore) / 4), 1)
                    : (double?)null
            }).ToListAsync();
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

        private IQueryable<MangaModel> ApplyMatureContentFilter(IQueryable<MangaModel> query, bool showMatureContent)
        {
            if (!showMatureContent)
            {
                // Assuming MatureSensitiveContent is a list of tag names considered mature
                query = query.Where(m => !m.GenresModels.Any(g => MatureSensetivContent.Contains(g.GenreName))
                                         && !m.TagsModels.Any(t => MatureSensetivContent.Contains(t.TagName)));
            }
            return query;
        }

        private IQueryable<MangaModel> ApplyAdultContentFilter(IQueryable<MangaModel> query, bool showAdultContent)
        {
            if (!showAdultContent)
            {
                // Assuming AdultSensitiveContent is a list of tag or genre names considered adult
                query = query.Where(m => !m.GenresModels.Any(g => adultSensetivContent.Contains(g.GenreName))
                                         && !m.TagsModels.Any(t => adultSensetivContent.Contains(t.TagName)));
            }
            return query;
        }
    }
}