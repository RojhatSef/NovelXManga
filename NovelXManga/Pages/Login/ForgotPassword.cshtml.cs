using EmailService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;

        private readonly IEmailSender _emailSender;

        public ForgotPasswordModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
        }

        [BindProperty]
        public ForgotPasswordViewModel forgotPasswordModel { get; set; }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost(ForgotPasswordViewModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);

            if (user == null)
            {
                return Page();
            }

            TempData["Message"] = "Email been sent";
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Page("/Login/ResetPasswordPage", "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);

            return Page();
        }

        public void OnGet()
        {
        }
    }
}