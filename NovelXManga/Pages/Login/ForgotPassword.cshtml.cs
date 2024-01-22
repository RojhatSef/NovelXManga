using EmailService;
using MangaAccessService;
using MangaAccessService.DTO.LoginRegiForgetDto;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    [ValidateAntiForgeryToken]
    public class ForgotPasswordModel : PageModel
    {
        private readonly UserManager<UserModel> userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMangaRepository mangaRepository;
        private readonly IEmailSender _emailSender;

        [BindProperty]
        public List<LoginRegiForgetCombineDto> AllBooksList { get; set; }

        public IEnumerable<LoginRegiForgetCombineDto> GetAllBooks { get; set; }

        public ForgotPasswordModel(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, IEmailSender emailSender, IMangaRepository mangaRepository)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            _emailSender = emailSender;
            this.mangaRepository = mangaRepository;
        }

        [BindProperty]
        public ForgotPasswordViewModel forgotPasswordModel { get; set; }

        public async Task<IActionResult> OnPost(ForgotPasswordViewModel forgotPasswordModel)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.FindByEmailAsync(forgotPasswordModel.Email);

            if (user == null)
            {
                GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(user);
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }

            TempData["Message"] = "Email been sent";
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            var callback = Url.Page("/Login/ResetPasswordPage", "Account", new { token, email = user.Email }, Request.Scheme);
            var message = new Message(new string[] { user.Email }, "Reset password token", callback, null);
            await _emailSender.SendEmailAsync(message);
            GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(user);
            AllBooksList = GetAllBooks.ToList();
            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            UserModel user = await userManager.GetUserAsync(User);
            GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(user);
            AllBooksList = GetAllBooks.ToList();
            return Page();
        }
    }
}