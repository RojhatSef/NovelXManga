using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        public string RoleName { get; set; }
    }
}
