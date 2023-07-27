using MangaAccessService;

namespace NovelXManga
{
    public class MangaRankingService
    {
        private readonly IMangaRepository _mangaRepository;
        private readonly IReadingListPageRepsitory _readingListPageRepsitory;
        private readonly IReviewRepsitory reviewRepsitory;

        public MangaRankingService(IMangaRepository mangaRepository, IReadingListPageRepsitory readingListPageRepsitory, IReviewRepsitory reviewRepsitory)
        {
            this._mangaRepository = mangaRepository;
            this._readingListPageRepsitory = readingListPageRepsitory;
            this.reviewRepsitory = reviewRepsitory;
        }

        public async Task UpdateRankingsAsync()
        {
            var allManga = await _mangaRepository.GetAllModelAsync();
            var rankedManga = allManga
                .Select(manga => new
                {
                    Manga = manga,
                    Score = manga.reviews.Count() + (manga.OverAllBookScore ?? 0)
                })
                .OrderByDescending(manga => manga.Score)
                .ToList();

            for (int i = 0; i < rankedManga.Count; i++)
            {
                rankedManga[i].Manga.Rank = i + 1;
                await _mangaRepository.UpdateMangaRankingAsync(rankedManga[i].Manga, rankedManga[i].Manga.Rank.Value);
            }
        }
    }
}