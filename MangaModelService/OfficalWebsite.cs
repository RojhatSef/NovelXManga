using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class OfficalWebsite
    {
        [Key]
        public int OfficalID { get; set; }
        [Required]
        public string WebsiteName { get; set; }
        [Required]
        public string OfficalWebsiteString { get; set; }

        public int? MangaID { get; set; }
        public MangaModel? mangaModel { get; set; }
        [ForeignKey("GroupScanlatingModel")]
        public int? GroupScanId { get; set; }
        public GroupScanlatingModel? groupScanlatingModel { get; set; }

        [ForeignKey("AuthorModel")]
        public int? AuthorID { get; set; }
        public AuthorModel? AuthorModel { get; set; }

        [ForeignKey("ArtistModel")]
        public int? ArtistId { get; set; }
        public ArtistModel? ArtistModel { get; set; }

        [ForeignKey("VoiceActorModel")]
        public int? VoiceActorId { get; set; }
        public VoiceActorModel? VoiceActorModel { get; set; }

        [ForeignKey("Character")]
        public int? CharacterId { get; set; }
        public Character? Characters { get; set; }
    }
}
