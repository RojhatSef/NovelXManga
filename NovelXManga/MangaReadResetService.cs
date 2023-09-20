using MangaAccessService;
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

        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _timer = new Timer(ResetMangaReadCounts, null, TimeSpan.Zero, TimeSpan.FromDays(1));
        //    return Task.CompletedTask;
        //}
        //public Task StartAsync(CancellationToken cancellationToken)
        //{
        //    _timer = new Timer(ResetMangaReadCounts, null, TimeSpan.Zero, TimeSpan.FromSeconds(20));
        //    return Task.CompletedTask;
        //}

        private async Task ResetMangaReadCountsAsync(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MangaNNovelAuthDBContext>();
                var mangaRepo = scope.ServiceProvider.GetRequiredService<IMangaRepository>();
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
                var now = DateTime.UtcNow;
                var mangas = await context.mangaModels.ToListAsync();

                foreach (var manga in mangas)
                {
                    string cacheKey = $"DailyRead_{manga.MangaID}";
                    if (cache.TryGetValue<int>(cacheKey, out int dailyRead))
                    {
                        manga.WeekRead += dailyRead;
                        manga.MonthRead += dailyRead;
                        manga.YearRead += dailyRead;
                        manga.ForeverRead += dailyRead;
                        await mangaRepo.UpdateAsync(manga);
                    }
                }
                foreach (var manga in mangas)
                {
                    string cacheKey = $"DailyRead_{manga.MangaID}";
                    cache.Remove(cacheKey);
                }
                foreach (var manga in mangas)
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

                    await mangaRepo.UpdateAsync(manga);
                }

                await context.SaveChangesAsync();
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