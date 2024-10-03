using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ChapterModel
    {
        [Key]
        public int ChapterID { get; set; }

        public DateTime DateRelease { get; set; }
        public DateTime UpdatedChapter { get; set; }
        public int? Volumes { get; set; }
        public int? ChapterNumber { get; set; }
        public string ChapterName { get; set; }
        public string ChapterLinkNumber { get; set; }
        public bool IsChecked { get; set; }

        public int GroupScanlatingID { get; set; }
        public virtual ICollection<GroupScanlatingModel> GroupScanlatingModels { get; set; }

        // Relationships
        public int? MangaID { get; set; }

        public MangaModel Manga { get; set; }

        // Navigation Property
        public ChapterContent ChapterContent { get; set; }
    }
}