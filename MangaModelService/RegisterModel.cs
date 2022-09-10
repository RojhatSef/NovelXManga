using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class RegisterModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm password is required")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "Password and Conformation password did not match")]
        public string ConfirmPassword { get; set; }
    }
}
