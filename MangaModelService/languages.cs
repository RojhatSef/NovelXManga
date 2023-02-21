using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class Languages
    {
        [Key]
        public int languageId { get; set; }

        [Required]
        public string LanguageName { get; set; }

        public int? MangaID { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }
        public virtual ICollection<BuyPage> BuyPages { get; set; }
    }
}