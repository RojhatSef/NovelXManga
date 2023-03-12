using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class FavoritBookList
    {
        [Key]
        public int FavoritBookListId { get; set; }

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel> FavoritBooks { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool isChecked { get; set; }
    }
}