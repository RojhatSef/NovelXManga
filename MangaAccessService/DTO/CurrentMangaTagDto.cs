using MangaModelService;
using System.ComponentModel.DataAnnotations;

namespace MangaAccessService.DTO
{
    public class CurrentMangaTagDto
    {
        [Key]
        public int TagId { get; set; }

        [Required]
        public string TagName { get; set; }

        public bool isChecked { get; set; }
        public string? TagDescription { get; set; }
        public int? TagHeavy { get; set; }
        public int? MangaID { get; set; }
        public virtual ICollection<MangaModel>? MangaModels { get; set; }
    }
}