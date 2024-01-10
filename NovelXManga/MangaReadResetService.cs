using MangaAccessService;
using MangaModelService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace NovelXManga
{
    public class MangaReadResetService : IHostedService, IDisposable
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public MangaReadResetService(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(async state => await ResetMangaReadCountsAsync(state), null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
            return Task.CompletedTask;
        }

        //what if the book does not have any users going to that page, now we are updating a page with nothing.
        // Also this needs a DTO, why are we fetching the entire mangamodels?
        private async Task ResetMangaReadCountsAsync(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MangaNNovelAuthDBContext>();
                var mangaRepo = scope.ServiceProvider.GetRequiredService<IMangaRepository>();
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
                var now = DateTime.UtcNow;

                var mangaIds = await context.mangaModels.Select(m => m.MangaID).ToListAsync();
                var updatedMangas = new List<MangaModel>(); // Store updated mangas

                foreach (var mangaId in mangaIds)
                {
                    string cacheKey = $"DailyRead_{mangaId}";
                    if (cache.TryGetValue<int>(cacheKey, out int dailyRead))
                    {
                        var manga = await context.mangaModels.FindAsync(mangaId);
                        manga.ForeverRead = (manga.ForeverRead ?? 0) + dailyRead;
                        manga.WeekRead += dailyRead;
                        manga.MonthRead += dailyRead;
                        manga.YearRead += dailyRead;

                        // Update the read dates if necessary
                        UpdateReadDates(manga, now);

                        updatedMangas.Add(manga);
                        cache.Remove(cacheKey);
                    }
                }

                if (updatedMangas.Any())
                {
                    context.mangaModels.UpdateRange(updatedMangas);
                    await context.SaveChangesAsync();
                }
            }
        }

        private void UpdateReadDates(MangaModel manga, DateTime now)
        {
            if (!manga.LastDailyReadDate.HasValue || (now - manga.LastDailyReadDate.Value).TotalDays > 1)
            {
                manga.DailyRead = 0;
                manga.LastDailyReadDate = now;
            }
            if (!manga.LastWeeklyReadDate.HasValue || (now - manga.LastWeeklyReadDate.Value).TotalDays > 7)
            {
                manga.WeekRead = 0;
                manga.LastWeeklyReadDate = now;
            }
            if (!manga.LastMonthlyReadDate.HasValue || (now - manga.LastMonthlyReadDate.Value).TotalDays > 30)
            {
                manga.MonthRead = 0;
                manga.LastMonthlyReadDate = now;
            }
            if (!manga.LastYearlyReadDate.HasValue || (now - manga.LastYearlyReadDate.Value).TotalDays > 365)
            {
                manga.YearRead = 0;
                manga.LastYearlyReadDate = now;
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}