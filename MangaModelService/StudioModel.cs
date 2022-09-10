using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class StudioModel
    {
        [Key]
        public int StudioId { get; set; }
        [Required]
        public string StudioName { get; set; }

        public string Biography { get; set; }
        public int Works { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }
    }
}
