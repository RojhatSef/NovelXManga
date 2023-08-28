using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class LoginIndexModel : PageModel
    {
        private readonly SignInManager<UserModel> signInManager;
        private readonly UserManager<UserModel> userManager;

        [BindProperty]
        public LoginModel LoginModel { get; set; }

        public string ReturnUrl { get; set; }

        public LoginIndexModel(SignInManager<UserModel> signInManager, UserManager<UserModel> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public IActionResult OnGet(string? returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");  // or any other appropriate page
            }

            ReturnUrl = returnUrl;
            ViewData["ReturnUrl"] = ReturnUrl;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");  // or any other appropriate page
            }
            returnUrl = ReturnUrl ?? Url.Content("/");
            if (ModelState.IsValid)
            {
                var identityResult = await signInManager.PasswordSignInAsync(LoginModel.Email, LoginModel.Password, LoginModel.RememberMe, false);
                if (identityResult.Succeeded)
                {
                    var user = await userManager.FindByEmailAsync(LoginModel.Email);
                    await signInManager.RefreshSignInAsync(user);
                    if (returnUrl == null || returnUrl == "/")
                    {
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        return RedirectToPage(returnUrl);
                    }
                }
                if (identityResult.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }

                ModelState.AddModelError("", "username or password incorrect");
            }
            ReturnUrl = returnUrl;
            return Page();
        }
    }
}