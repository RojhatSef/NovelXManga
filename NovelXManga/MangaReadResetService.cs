using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using MangaAccessService;
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
            _timer = new Timer(ResetMangaReadCounts, null, TimeSpan.Zero, TimeSpan.FromDays(1));
            return Task.CompletedTask;
        }

        private void ResetMangaReadCounts(object state)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MangaNNovelAuthDBContext>();
                var mangaRepo = scope.ServiceProvider.GetRequiredService<IMangaRepository>();
                var cache = scope.ServiceProvider.GetRequiredService<IMemoryCache>();
                var now = DateTime.UtcNow;
                var mangas = context.mangaModels.ToList();

                foreach (var manga in mangas)
                {
                    string cacheKey = $"DailyRead_{manga.MangaID}";
                    if (cache.TryGetValue<int>(cacheKey, out int dailyRead))
                    {
                        manga.WeekRead += dailyRead;
                        manga.MonthRead += dailyRead;
                        manga.YearRead += dailyRead;
                        manga.ForeverRead += dailyRead;
                        mangaRepo.UpdateAsync(manga);
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

                    mangaRepo.Update(manga);
                }

                context.SaveChanges();
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