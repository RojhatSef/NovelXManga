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
        public string? PhotoPath { get; set; }
        public string? Lawful { get; set; }
        public string? Personality { get; set; }
        public string? FamousQuote { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? EyeColor { get; set; }
        public string? HairColor { get; set; }
        public string? Abilities { get; set; }
        public string? Hobbies { get; set; }
        public string? Dislikes { get; set; }
        public string? Likes { get; set; }
        public string? PersonalityTraits { get; set; }
        public bool isChecked { get; set; }

        // Missing data Icollection of Artist/Author/Voice Actor. When pressing a character
        // the user should be able to see who drew this character, who voice acted, and who wrote it.
        // public virtual ICollection<AuthorModel> CharacterAuthor { get; set; }
        // public virtual ICollection<VoiceActorModel> VoiceActors { get; set; }
        // public virtual ICollection<ArtistModel> ArtistModel { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }

        public virtual ICollection<Character>? Partner_s { get; set; }
        public virtual ICollection<Character>? Family { get; set; }

        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }

        public virtual ICollection<MangaModel>? MangaModels { get; set; }
    }
}