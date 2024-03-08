namespace MangaAccessService.DTO
{
    public class CharacterDTO
    {
        public int CharacterId { get; set; }
        public string CharacterName { get; set; }
        public string PhotoPath { get; set; }
        public List<OfficalWebsiteDTO> OfficalWebsites { get; set; }
        public List<AssociatedNameDTO> AssociatedNames { get; set; }
    }
}