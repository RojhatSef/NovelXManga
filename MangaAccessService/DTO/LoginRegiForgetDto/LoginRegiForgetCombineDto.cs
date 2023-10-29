namespace MangaAccessService.DTO.LoginRegiForgetDto
{
    public class LoginRegiForgetCombineDto
    {
        public int MangaID { get; set; }
        public string? PhotoPath { get; set; }
        public double? OverAllBookScore { get; set; }
        public List<LoginRegiForgetArtistDto>? ArtistModels { get; set; }
        public List<LoginRegiForgetAuthorDto>? Authormodels { get; set; }

        public List<LoginRegiForgetGenresDto>? GenresModels { get; set; }
        public List<LoginRegiForgetTagsDto>? TagsModels { get; set; }
    }
}