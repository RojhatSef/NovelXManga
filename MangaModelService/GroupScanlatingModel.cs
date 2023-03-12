using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class GroupScanlatingModel
    {
        [Key]
        public int GroupScanlatingID { get; set; }

        [Required]
        public string GroupName { get; set; }

        public string? PhotoPath { get; set; }
        public string? website { get; set; }
        public DateTime? CreatedDate { get; set; }
        public virtual ICollection<ChapterModel>? chapterModels { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel>? MangaModels { get; set; }
        public string? userID { get; set; }
        public virtual ICollection<UserModel>? userModels { get; set; }
        public bool IsChecked { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}