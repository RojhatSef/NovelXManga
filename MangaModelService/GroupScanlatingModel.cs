using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class GroupScanlatingModel
    {
        [Key]
        public int? GroupScanlatingID { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string? PhotoPath { get; set; }
        public string? website { get; set; }
        public ICollection<ChapterModel> chapterModels { get; set; }
        public int? masterId { get; set; }
        public ICollection<MasterModel> MasterModels { get; set; }
        public string userID { get; set; }
        public ICollection<UserModel>? userModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
