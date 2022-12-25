using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class BlogModel
    {
        [Key]
        public int BlogModelId { get; set; }
        public string mangaName { get; set; }
        public virtual ICollection<PostModel>? postsModel { get; set; }

        public int MangaId { get; set; }
        [Required]
        public MangaModel MangaModel { get; set; }
    }
}
