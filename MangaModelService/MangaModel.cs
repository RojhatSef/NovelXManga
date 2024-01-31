using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class MangaModel
    {
        [Key]
        public int MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried")]
        [MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]
        [MaxLength(255, ErrorMessage = "Name must not exceed 255 characters")]
        public string MangaName { get; set; }

        [StringLength(10, ErrorMessage = "ISBN10 must be 10 characters", MinimumLength = 10)]
        public string? ISBN10 { get; set; }

        [StringLength(13, ErrorMessage = "ISBN13 must be 13 characters", MinimumLength = 13)]
        public string? ISBN13 { get; set; }

        [MaxLength(1000, ErrorMessage = "futureEvents must not exceed 1000 characters")]
        public string? futureEvents { get; set; }

        [MaxLength(100, ErrorMessage = "StatusInCountryOfOrigin must not exceed 100 characters")]
        public string? StatusInCountryOfOrigin { get; set; }

        [MaxLength(100, ErrorMessage = "CompletelyTranslated must not exceed 100 characters")]
        public string? CompletelyTranslated { get; set; }

        [MaxLength(500, ErrorMessage = "orignalWebtoon must not exceed 500 characters")]
        public string? orignalWebtoon { get; set; }

        [MaxLength(200, ErrorMessage = "OriginalPublisher must not exceed 200 characters")]
        public string? OriginalPublisher { get; set; }

        public int? Rank { get; set; }
        public int? PageView { get; set; }

        [MaxLength(100, ErrorMessage = "Type must not exceed 100 characters")]
        public string? Type { get; set; }

        [MaxLength(100, ErrorMessage = "OfficalLanguage must not exceed 100 characters")]
        public string? OfficalLanguage { get; set; }

        public string? PhotoPath { get; set; }
        public DateTime? LastDailyReadDate { get; set; }
        public DateTime? LastWeeklyReadDate { get; set; }
        public DateTime? LastMonthlyReadDate { get; set; }
        public DateTime? LastYearlyReadDate { get; set; }
        public int? DailyRead { get; set; }
        public int? WeekRead { get; set; }
        public int? MonthRead { get; set; }
        public int? YearRead { get; set; }
        public int? ForeverRead { get; set; }

        [MaxLength(5000, ErrorMessage = "Description must not exceed 5000 characters")]
        public string? Description { get; set; }

        public DateTime? ReleaseYear { get; set; }
        public DateTime? EndingYear { get; set; }
        public DateTime? BookAddedToDB { get; set; }
        public DateTime? BookUpdated { get; set; }
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

        //public ICollection<ScoreDistributionEntry> ScoreDistribution { get; set; }
        // public Dictionary<int, int> NewScore { get; set; }
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