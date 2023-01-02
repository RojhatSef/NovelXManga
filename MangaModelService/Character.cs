using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class Character
    {
        [Key]
        public int CharacterId { get; set; }
        [Required]
        public string CharacterName { get; set; }
        public string? Background { get; set; }
        public string? Gender { get; set; }
        public string? Born { get; set; }
        public string? Death { get; set; }
        public string? specie { get; set; }
        public string? PlaceOffResidence { get; set; }
        public string? World { get; set; }
        public string? Nationality { get; set; }
        public string? Education { get; set; }
        public string? Occupation { get; set; }
        public string? Lawful { get; set; }
        public string? Personality { get; set; }
        public string? FamousQuote { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }
        public virtual ICollection<Character>? Partner_s { get; set; }
        public virtual ICollection<Character>? Family { get; set; }


        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }

        public virtual ICollection<MangaModel>? MangaModels { get; set; }

    }
}
