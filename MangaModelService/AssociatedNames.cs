using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class AssociatedNames
    {
        [Key]
        public int AssociatedNamesId { get; set; }
        public string? nameString { get; set; }
        public int? MangaID { get; set; }
        public MangaModel? mangaModel { get; set; }

        [ForeignKey("AuthorModel")]
        public int? AuthorID { get; set; }
        public AuthorModel AuthorModel { get; set; }

        [ForeignKey("ArtistModel")]
        public int? ArtistId { get; set; }
        public ArtistModel ArtistModel { get; set; }

        [ForeignKey("VoiceActorModel")]
        public int? VoiceActorId { get; set; }
        public VoiceActorModel VoiceActorModel { get; set; }
    }
}
