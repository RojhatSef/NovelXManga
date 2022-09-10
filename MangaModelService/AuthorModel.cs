using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class AuthorModel
    {
        [Key]
        public int AuthorId { get; set; }
        [Required]
        public string AuthorName { get; set; }

        public string Biography { get; set; }
        public string Works { get; set; }
        public int AmountOfWork { get; set; }

        public ICollection<MasterModel> MasterModels { get; set; }
        //ICollection<MangaModel> mangaModels { get; set; }
    }
}
