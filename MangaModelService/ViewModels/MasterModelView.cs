using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class MasterModelView
    {

        public int MasterID { get; set; }

        public string GroupScanlating { get; set; }
        public string userModels { get; set; }

        [Required]
        public MangaModel MangaModels { get; set; }
    }
}
