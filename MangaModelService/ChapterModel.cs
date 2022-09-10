using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ChapterModel
    {
        [Key]
        public int chapterID { get; set; }
        public DateTime dateRelease { get; set; }
        public int? Volumes { get; set; }
        public string chapterName { get; set; }
        public int GroupScanlatingID { get; set; }
        public string chapterLinkNumber { get; set; }
        public GroupScanlatingModel GroupScanlatingModel { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }
        //public int MangaId { get; set; }
        //public MangaModel mangaModel { get; set; }
    }
}
