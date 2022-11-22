using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class MangaModelView
    {

        public int? MangaID { get; set; }

        [Required(ErrorMessage = "Name is requried"),
        MinLength(2, ErrorMessage = "Name must contain at least 2 characters")]

        public string MangaName { get; set; }
        public string? AssociatedNames { get; set; }
        public string? Type { get; set; }

        public string? PhotoPath { get; set; }

        public string? Description { get; set; }
        public DateTime ReleaseYear { get; set; }


        public int? BlogModelID { get; set; }
        public BlogModel? BlogModel { get; set; }

        public int? MasterModelID { get; set; }
        public MasterModel? MasterModels { get; set; }
        public string? GenresModels { get; set; }
        public string? TagsModels { get; set; }
    }
}
