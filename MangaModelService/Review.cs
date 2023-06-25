using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class Review
    {
        [Key]
        public int ReviewID { get; set; }

        [Required]
        public double BookScore { get; set; }

        [Required]
        public double voteReview { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(750)]
        public string Content { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public virtual ICollection<MangaModel> MangaModels { get; set; }
        public bool IsChecked { get; set; }
    }
}