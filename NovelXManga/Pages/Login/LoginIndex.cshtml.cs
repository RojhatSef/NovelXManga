using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class LoginIndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signInManager;

        [BindProperty]
        public LoginModel LoginModel { get; set; }
        public LoginIndexModel(SignInManager<IdentityUser> signInManager)
        {
            this.signInManager = signInManager;
        }
        public void OnGet()
        {
        }
        //public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var identityResult = await signInManager.PasswordSignInAsync(Model.Email, Model.Password, Model.RememberMe, false);
        //        if (identityResult.Succeeded)
        //        {
        //            if (returnUrl == null || returnUrl == "/")
        //            {
        //                return RedirectToPage("Index");
        //            }
        //            else
        //            {
        //                return RedirectToPage(returnUrl);
        //            }
        //        }
        //    }
        //    ModelState.AddModelError("", "username or password incorrect");
        //    return Page();
        //}
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, false);
                if (identityResult.Succeeded)
                {

                    return RedirectToPage("/Login/LoginIndex");

                }
            }
            ModelState.AddModelError("", "username or password incorrect");
            return Page();
        }
    }
}
