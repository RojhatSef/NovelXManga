using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ArtistModel
    {
        [Key]
        public int ArtistId { get; set; }
        [Required]
        public string ArtistName { get; set; }

        public string Biography { get; set; }
        public int Works { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }

    }
}
