using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Login
{
    public class ResetPasswordPageModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMangaRepository mangaRepository;
        public List<MangaModel> AllBooksList { get; set; }
        public IEnumerable<MangaModel> GetAllBooks { get; set; }
        public ResetPasswordModelView tempResetpass { get; set; }

        [TempData]
        public string TempDataSuccededPassWordChange { get; set; }

        public ResetPasswordPageModel(UserManager<UserModel> userManager, IMangaRepository mangaRepository)
        {
            this._userManager = userManager;
            this.mangaRepository = mangaRepository;
        }

        [BindProperty]
        public ResetPasswordModelView model { get; set; }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost(ResetPasswordModelView resetPasswordModelView)
        {
            model.Email = resetPasswordModelView.Email;
            model.Token = resetPasswordModelView.Token;
            if (model.Password == null)
            {
                GetAllBooks = await mangaRepository.Get10MangaModelAsync();
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }
            if (model.ConfirmPassword == null)
            {
                GetAllBooks = await mangaRepository.Get10MangaModelAsync();
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModelView.Email);
            if (user == null)
            {
                RedirectToPage("/Pages/Login/ForgotPassword");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                TempDataSuccededPassWordChange = "Password Changed Successfully, Login as usual";
                GetAllBooks = await mangaRepository.Get10MangaModelAsync();
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnGetAsync(ResetPasswordModelView resetPasswordModel)
        {
            tempResetpass = new ResetPasswordModelView { Token = resetPasswordModel.Token, Email = resetPasswordModel.Email };

            GetAllBooks = await mangaRepository.Get10MangaModelAsync();
            AllBooksList = GetAllBooks.ToList();
            return Page();
        }
    }
}