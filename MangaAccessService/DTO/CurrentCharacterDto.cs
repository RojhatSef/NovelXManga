namespace MangaAccessService.DTO
{
    public class CurrentCharacterDto
    {
        public int CharacterId { get; set; }
        public List<CurrentCharacterMangaDto>? MangaList { get; set; }
    }
}