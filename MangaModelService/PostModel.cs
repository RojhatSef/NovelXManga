using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class PostModel
    {
        [Key]
        public int? PostId { get; set; }

        public string Title { get; set; }
        public string postComment { set; get; }

        public double? score { get; set; }
        public DateTime CommentPostedTime { get; set; }

        public int BlogId { get; set; }
        public BlogModel BlogModel { get; set; }

        public string? UserID { get; set; }
        public virtual UserModel? UserModel { get; set; }
    }

}
