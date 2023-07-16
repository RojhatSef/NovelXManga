using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    [Authorize]
    public class LogOutModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;

        public LogOutModel(SignInManager<UserModel> signInManager)
        {
            this.signInManager = signInManager;
        }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostLogOutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }

        public IActionResult OnPostDontLogOutAsync()
        {
            return RedirectToPage("/Index");
        }
    }
}