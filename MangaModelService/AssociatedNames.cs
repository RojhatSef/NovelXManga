using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class AssociatedNames
    {
        [Key]
        public int AssociatedNamesId { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = "Name must contain at least 1 characters")]
        [MaxLength(1500, ErrorMessage = "Alternative name must not exceed 1500 characters")]
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

        [ForeignKey("Character")]
        public int? CharacterId { get; set; }

        public Character Characters { get; set; }
        public bool isChecked { get; set; }
    }
}