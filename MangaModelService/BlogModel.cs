using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class BlogModel
    {
        [Key]
        public int id { get; set; }
        public string mangaName { get; set; }
        public ICollection<PostModel>? postsModel { get; set; }
        public int mangaId { get; set; }
        public MangaModel MangaModel { get; set; }
    }
}
