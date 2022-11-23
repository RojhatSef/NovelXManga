using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class BlogModel
    {
        [Key]
        public int BlogModelId { get; set; }
        public string mangaName { get; set; }
        public ICollection<PostModel>? postsModel { get; set; }

        [Required]
        public MangaModel MangaModel { get; set; }
    }
}
