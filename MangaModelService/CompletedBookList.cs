using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class CompletedBookList
    {
        [Key]
        public int CompletedBookListID { get; set; }

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel> ReadingMangaList { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool isChecked { get; set; }
    }
}