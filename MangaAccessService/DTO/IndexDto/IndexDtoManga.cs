namespace MangaAccessService.DTO.IndexDto
{
    public class IndexDtoManga
    {
        public int MangaID { get; set; }
        public string MangaName { get; set; }
        public string? PhotoPath { get; set; }
        public double? OverAllBookScore { get; set; }
        public int? ForeverRead { get; set; }
        public string? Description { get; set; }
        public List<IndexArtistDto>? ArtistModels { get; set; }
        public List<IndexAuthorDto>? Authormodels { get; set; }

        public List<IndexGenreDto>? GenresModels { get; set; }
        public List<IndexTagsDto>? TagsModels { get; set; }
    }
}