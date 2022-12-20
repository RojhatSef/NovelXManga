using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class MangaModel
    {
        [Key]
        public int MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]

        public string MangaName { get; set; }
        public ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public List<MangaModel>? relatedSeries { get; set; }
        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string? futureEvents { get; set; }
        public string? StatusInCountryOfOrigin { get; set; }
        public string? CompletelyTranslated { get; set; }
        public string? orignalWebtoon { get; set; }
        public string? OriginalPublisher { get; set; }
        public string? OfficialTranslations { get; set; }
        public double? score { get; set; }
        public string? Type { get; set; }
        public string? OfficalLanguage { get; set; }
        public string? PhotoPath { get; set; }

        public string? Description { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public DateTime? EndingYear { get; set; }
        public List<MangaModel>? RecommendedMangaModels { get; set; }
        public int BlogModelId { get; set; }
        public BlogModel BlogModel { get; set; }

        public int MasterID { get; set; }
        public MasterModel MasterModels { get; set; }
        public ICollection<AuthorModel>? Authormodels { get; set; }
        public ICollection<ArtistModel>? ArtistModels { get; set; }
        public ICollection<VoiceActorModel>? VoiceActors { get; set; }

        public ICollection<GenresModel>? GenresModels { get; set; }
        public ICollection<TagModel>? TagsModels { get; set; }



    }
}
