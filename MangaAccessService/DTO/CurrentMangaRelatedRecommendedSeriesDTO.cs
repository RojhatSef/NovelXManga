namespace MangaAccessService.DTO
{
    public class CurrentMangaRelatedRecommendedSeriesDTO
    {
        public List<CurrentMangaRelatedSeriesDTO> RelatedSeries { get; set; }
        public List<MangaModelDTO> RecommendedMangaModels { get; set; }
    }
}