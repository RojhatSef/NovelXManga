namespace MangaAccessService.DTO
{
    public class CurrentMangaDto
    {
        public int MangaID { get; set; }
        public List<CurrentMangaArtistDto>? Artists { get; set; }
        public List<CurrentMangaAuthorDto>? Authors { get; set; }
        public List<CurrentMangaCharacterDto>? Characters { get; set; }
        public List<CurrentMangaVoiceActorDto>? VoiceActors { get; set; }
        public List<CurrentMangaGenreDto>? Genres { get; set; }
        public List<CurrentMangaTagDto>? Tags { get; set; }
    }
}