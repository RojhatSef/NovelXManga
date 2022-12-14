using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class MangaModelView
    {

        public int? MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]

        public string MangaName { get; set; }
        public string? AssociatedNames { get; set; }
        public string? Type { get; set; }

        public string? PhotoPath { get; set; }
        public string? relatedSeries { get; set; }
        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string? futureEvents { get; set; }
        public string? StatusInCountryOfOrigin { get; set; }
        public string? CompletelyTranslated { get; set; }
        public string? orignalWebtoon { get; set; }
        public string? OriginalPublisher { get; set; }
        public string? OfficialTranslations { get; set; }
        public double? score { get; set; }

        public string? Description { get; set; }
        public DateTime? ReleaseYear { get; set; }

        public DateTime? EndingYear { get; set; }
        public List<MangaModel>? RecommendedMangaModels { get; set; }
        public int? BlogModelId { get; set; }
        public BlogModel? BlogModel { get; set; }

        public int? MasterID { get; set; }
        public MasterModel? MasterModels { get; set; }
        public string? GenresModels { get; set; }
        public string? TagsModels { get; set; }
    }
}
