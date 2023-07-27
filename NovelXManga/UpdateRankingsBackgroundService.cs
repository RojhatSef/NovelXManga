namespace NovelXManga
{
    public class UpdateRankingsBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private const int Daily = 24 * 60 * 60 * 1000; // milliseconds in a day

        public UpdateRankingsBackgroundService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var mangaRankingService = scope.ServiceProvider.GetRequiredService<MangaRankingService>();
                    await mangaRankingService.UpdateRankingsAsync();
                }

                await Task.Delay(Daily, stoppingToken);
            }
        }
    }
}