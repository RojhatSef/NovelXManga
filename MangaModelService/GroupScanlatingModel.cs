using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class GroupScanlatingModel
    {
        [Key]
        public int GroupScanlatingID { get; set; }
        [Required]
        public string GroupScanlatingName { get; set; }
        public string? website { get; set; }
        public ICollection<ChapterModel> chapterModels { get; set; }
        public ICollection<MasterModel> MasterModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
