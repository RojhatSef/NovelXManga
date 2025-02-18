﻿using MangaAccessService;

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

        // incorrect, we need to check if the manga even needs an update, if it does not why should we update it.
        //fix a bool that checks if it's a need of update.
        public async Task UpdateRankingsAsync()
        {
            var allManga = await _mangaRepository.GetAllModelAsync();

            // Calculate the score distribution for each manga

            // Rank the manga based on a calculated score
            var rankedManga = allManga
                .OrderByDescending(manga => manga.reviews.Count() + (manga.OverAllBookScore ?? 0))
                .ToList();

            for (int i = 0; i < rankedManga.Count; i++)
            {
                rankedManga[i].Rank = i + 1;
            }

            // Update each manga record in the database
            foreach (var manga in allManga)
            {
                await _mangaRepository.UpdateAsync(manga);
            }
        }
    }
}