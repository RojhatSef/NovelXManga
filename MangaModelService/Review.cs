using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        [Range(0, 10)]
        public double StylesScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double StoryScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double GrammarScore { get; set; }

        [Required]
        [Range(0, 10)]
        public double CharactersScore { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(750)]
        public string Content { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public virtual ICollection<MangaModel> MangaModels { get; set; }
        public bool IsChecked { get; set; }

        [NotMapped]
        public double OverAllBookScore
        {
            get
            {
                return Math.Round((StylesScore + StoryScore + GrammarScore + CharactersScore) / 4.0, 1);
            }
        }
    }
}