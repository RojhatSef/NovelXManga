using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class MangaModel
    {
        [Key]
        public int MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]
        public string MangaName { get; set; }

        public string? ISBN10 { get; set; }
        public string? ISBN13 { get; set; }
        public string? futureEvents { get; set; }
        public string? StatusInCountryOfOrigin { get; set; }
        public string? CompletelyTranslated { get; set; }
        public string? orignalWebtoon { get; set; }
        public string? OriginalPublisher { get; set; }
        public int? Rank { get; set; }
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
        public virtual List<MangaModel>? relatedSeries { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public virtual ICollection<Languages>? AllLanguages { get; set; }
        public virtual List<MangaModel>? RecommendedMangaModels { get; set; }
        public int BlogModelId { get; set; }
        public BlogModel BlogModel { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }
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
        public bool IsChecked { get; set; }

        [NotMapped]
        public Dictionary<int, int> ScoreDistribution
        {
            get
            {
                var distribution = new Dictionary<int, int>();
                if (reviews != null && reviews.Any())
                {
                    for (int i = 1; i <= 5; i++)
                    {
                        distribution[i] = reviews.Count(r => Math.Round(r.OverAllBookScore) == i && r.MangaModels.Any(m => m.MangaID == this.MangaID));
                    }
                }
                return distribution;
            }
        }

        public double? OverAllBookScore
        {
            get
            {
                if (reviews != null && reviews.Any())
                {
                    return Math.Round(reviews.Average(r => (r.StylesScore + r.StoryScore + r.GrammarScore + r.CharactersScore) / 4), 1);
                }
                else
                {
                    return null;
                }
            }
            //get  // system Bayesian DONT DELETE THIS!!!!
            //{
            //    double m = 50; // Choose your own threshold for minimum reviews to be considered in top list
            //    double C = 3; // Choose your own average rating across all books
            //    int v = Reviews.Count;
            //    double R = Math.Round(Reviews.Average(r => (r.StylesScore + r.StoryScore + r.GrammarScore + r.CharactersScore) / 4), 1);

            //    if (v > m)
            //    {
            //        return (v / (v + m)) * R + (m / (v + m)) * C;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }
    }
}