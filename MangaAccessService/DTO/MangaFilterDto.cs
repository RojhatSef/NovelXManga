namespace MangaAccessService.DTO
{
    public class MangaFilterDto
    {
        public int MangaID { get; set; }
        public string MangaName { get; set; }
        public string ISBN10 { get; set; }
        public string ISBN13 { get; set; }
        public string Type { get; set; }
        public string PhotoPath { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public DateTime? EndingYear { get; set; }
        public ICollection<string> AssociatedNames { get; set; }
        public ICollection<string> Genres { get; set; }
        public ICollection<string> Tags { get; set; }
        public ICollection<string> Characters { get; set; }
        public ICollection<string> Authors { get; set; }
        public ICollection<string> Artists { get; set; }
        public ICollection<string> VoiceActors { get; set; }
    }
}