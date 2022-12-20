using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class VoiceActorModel
    {
        [Key]
        public int VoiceAcotrId { get; set; }
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhotoPath { get; set; }
        public string? Gender { get; set; }
        public string? officalWebsite { get; set; }
        public string? Twitter { get; set; }
        public string? reddit { get; set; }
        public string? BirthPlace { get; set; }

        public string? Biography { get; set; }
        public string? NameInNative { get; set; }
        public string? WikiPedia { get; set; }
        public ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public int? AmountOfWork { get; set; }
        public string? WorkingAt { get; set; }
        public DateTime? LastUpdate { get; set; }

        public DateTime? VoiceBorn { get; set; }
        public DateTime? VoiceDeath { get; set; }

        public string? Contact { get; set; }
        public int? MangaID { get; set; }
        public MangaModel? mangaModel { get; set; }

    }
}
