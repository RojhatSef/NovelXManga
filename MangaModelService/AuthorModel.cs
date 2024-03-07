using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class AuthorModel
    {
        [Key]
        public int AuthorID { get; set; }

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

        // Missing data Icollection of Characters
        // public virtual ICollection<Character> CreatorsCharacters { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }

        public int? AmountOfWork { get; set; }
        public string? WorkingAt { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime? AuthorBorn { get; set; }
        public DateTime? AuthorDeath { get; set; }
        public string? Contact { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }
        public bool isChecked { get; set; }
    }
}