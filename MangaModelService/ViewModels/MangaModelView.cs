using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class MangaModelView
    {
        [Key]
        public int MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]
        public string MangaName { get; set; }

        public virtual List<MangaModel>? relatedSeries { get; set; }
        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string? futureEvents { get; set; }
        public string? StatusInCountryOfOrigin { get; set; }
        public string? CompletelyTranslated { get; set; }
        public string? orignalWebtoon { get; set; }
        public string? OriginalPublisher { get; set; }
        public double? Rank { get; set; }
        public int? PageView { get; set; }
        public string? Type { get; set; }
        public string? OfficalLanguage { get; set; }
        public string? PhotoPath { get; set; }
        public int? MonthRead { get; set; }
        public int? WeekRead { get; set; }
        public int? YearRead { get; set; }
        public string? Description { get; set; }
        public DateTime? ReleaseYear { get; set; }
        public DateTime? EndingYear { get; set; }
        public DateTime? BookAddedToDB { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public virtual ICollection<Languages> AllLanguages { get; set; }
        public virtual List<MangaModel>? RecommendedMangaModels { get; set; }
        public int BlogModelId { get; set; }
        public BlogModel BlogModel { get; set; }
        public virtual ICollection<OfficalWebsite> OfficalWebsites { get; set; }
        public virtual ICollection<AuthorModel>? Authormodels { get; set; }
        public virtual ICollection<ArtistModel>? ArtistModels { get; set; }
        public virtual ICollection<VoiceActorModel>? VoiceActors { get; set; }
        public int? GroupScanlatingID { get; set; }
        public virtual ICollection<GroupScanlatingModel>? GroupScanlating { get; set; }
        public string? userId { get; set; }
        public virtual ICollection<UserModel>? userModels { get; set; }
        public virtual ICollection<StudioModel>? StudioModels { get; set; }
        public virtual ICollection<GenresModel>? GenresModels { get; set; }
        public virtual ICollection<TagModel>? TagsModels { get; set; }
        public virtual ICollection<Character>? Characters { get; set; }
        public virtual ICollection<Review>? reviews { get; set; }
        public virtual ICollection<BuyPage>? BuyPages { get; set; }
    }
}