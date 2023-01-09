using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class DroppedBookList
    {
        [Key]
        public int DroppedId { get; set; }
        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }
        public virtual ICollection<MangaModel> ReadingMangaList { get; set; }
        [ForeignKey("UserModel")]
        public string? UserId { get; set; }
        public virtual ICollection<UserModel> UserModels { get; set; }
    }
}
