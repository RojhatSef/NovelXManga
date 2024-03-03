using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ArtistModel
    {
        [Key]
        public int ArtistId { get; set; }

        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }
        public string? PhotoPath { get; set; }
        public string? Gender { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }
        public string? Twitter { get; set; }
        public string? reddit { get; set; }
        public string? BirthPlace { get; set; }
        public string? Biography { get; set; }
        public string? NameInNative { get; set; }
        public string? WikiPedia { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public int? AmountOfWork { get; set; }
        public string? WorkingAt { get; set; }
        public DateTime? ArtistBorn { get; set; }
        public DateTime? ArtistDeath { get; set; }
        public string? Contact { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }

        // Missing data Icollection of Character
        public bool isChecked { get; set; }
    }
}