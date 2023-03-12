using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class BuyPage
    {
        [Key]
        public int Id { get; set; }

        public virtual ICollection<Languages>? _Languages { get; set; }
        public virtual ICollection<MangaModel>? _MangaModels { get; set; }

        [Required]
        public string BuyWebsite { get; set; }

        public bool isChecked { get; set; }
    }
}