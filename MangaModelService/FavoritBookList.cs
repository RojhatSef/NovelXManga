using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class FavoritBookList
    {
        [Key]
        public int FavoritBookListId { get; set; }

        public string CompletedList { get; set; } = "Favorit List";

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel>? FavoritBooks { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool isChecked { get; set; }
        public bool ShowFavoritList { get; set; } = true;
    }
}