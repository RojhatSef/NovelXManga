using System.ComponentModel.DataAnnotations;

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
        public virtual ICollection<ChapterModel>? chapterModels { get; set; }
        public virtual ICollection<AssociatedNames>? AssociatedNames { get; set; }
        public virtual ICollection<OfficalWebsite>? OfficalWebsites { get; set; }
        public int? MasterID { get; set; }
        public virtual ICollection<MasterModel>? MasterModels { get; set; }
        public string? userID { get; set; }
        public virtual ICollection<UserModel>? userModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
