using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class ForgottPasswordsModel : PageModel
    {
        [TempData]
        public string SentEmail { get; set; }
        public ForgottPasswordModel ForgottPasswordModel { get; set; }
        public IActionResult ForgottPassword(ForgottPasswordModel forgottPasswordModel)
        {
            if (ModelState.IsValid)
            {
                ModelState.Clear();
                SentEmail = "Email Has Been Sent To your Registerade Email";
            }
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
