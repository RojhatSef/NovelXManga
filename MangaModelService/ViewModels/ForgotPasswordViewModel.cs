using System.ComponentModel.DataAnnotations;

namespace MangaModelService.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}