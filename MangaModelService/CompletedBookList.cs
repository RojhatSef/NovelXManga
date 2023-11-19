using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MangaModelService
{
    public class CompletedBookList
    {
        [Key]
        public int CompletedBookListID { get; set; }

        public string CompletedList { get; set; } = "Completed List";

        [ForeignKey("MangaModel")]
        public int? MangaModelId { get; set; }

        public virtual ICollection<MangaModel>? CompleteBookList { get; set; }

        [ForeignKey("UserModel")]
        public string? UserId { get; set; }

        public virtual ICollection<UserModel> UserModels { get; set; }
        public bool isChecked { get; set; }
        public bool ShowCompletedList { get; set; } = true;
    }
}