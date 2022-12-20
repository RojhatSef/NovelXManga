using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class AssociatedNames
    {
        [Key]
        public int AssociatedNamesId { get; set; }
        public string? nameString { get; set; }
    }
}
