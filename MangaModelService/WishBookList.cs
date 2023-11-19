using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class WishBookList
    {
        [Key]
        public int WishBookListId { get; set; }

        public string WishNameList { get; set; } = "Wish List";

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel> WishBooks { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool? IsChecked { get; set; }
        public bool ShowWishList { get; set; } = true;
    }
}