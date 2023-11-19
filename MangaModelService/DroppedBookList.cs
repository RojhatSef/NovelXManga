using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class DroppedBookList
    {
        [Key]
        public int DroppedId { get; set; }

        public string DroppedNameList { get; set; } = "Dropped Book List";

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel>? _DroppedBooks { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool isChecked { get; set; }
        public bool ShowDroppedList { get; set; } = true;
    }
}