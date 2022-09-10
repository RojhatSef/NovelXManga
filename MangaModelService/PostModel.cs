using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class PostModel
    {
        [Key]
        public int PostId { get; set; }

        public string Title { get; set; }
        public string postComment { set; get; }


        public ICollection<MasterModel> MasterModels { get; set; }
        //public int MangaId { get; set; }
        //public MangaModel mangaModel { get; set; }
    }

}
