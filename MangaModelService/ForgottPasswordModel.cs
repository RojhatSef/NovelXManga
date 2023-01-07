using System.ComponentModel.DataAnnotations;

namespace MangaModelService
{
    public class ForgottPasswordModel
    {
        [Required, EmailAddress, Display(Name = "Registerad Email Adress")]
        public string Email { get; set; }


    }
}
