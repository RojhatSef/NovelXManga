using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;
using MangaAccessService;

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
                var now = DateTime.UtcNow;
                var mangas = context.mangaModels.ToList();

                foreach (var manga in mangas)
                {
                    bool updateDate = false;

                    // Ensure every manga has a LastReadDate, if not set it to the current date
                    if (!manga.LastReadDate.HasValue)
                    {
                        manga.LastReadDate = now;
                    }
                    else
                    {
                        if (now.Date > manga.LastReadDate.Value.Date.AddDays(1))
                        {
                            manga.DailyRead = 0;
                            updateDate = true;
                        }

                        if ((now - manga.LastReadDate.Value).TotalDays > 7)
                        {
                            manga.WeekRead = 0;
                            updateDate = true;
                        }

                        if ((now - manga.LastReadDate.Value).TotalDays > 30)
                        {
                            manga.MonthRead = 0;
                            updateDate = true;
                        }

                        if ((now - manga.LastReadDate.Value).TotalDays > 365)
                        {
                            manga.YearRead = 0;
                            updateDate = true;
                        }

                        // Set LastReadDate to now, if any of the conditions above were true
                        if (updateDate)
                        {
                            manga.LastReadDate = now;
                        }
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