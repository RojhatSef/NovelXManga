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
    public class ResetPasswordPageModel : PageModel
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> signInManager;
        private readonly IMangaRepository mangaRepository;
        private readonly RoleManager<IdentityRole> roleManager;

        [BindProperty]
        public List<LoginRegiForgetCombineDto> AllBooksList { get; set; }

        public IEnumerable<LoginRegiForgetCombineDto> GetAllBooks { get; set; }
        public ResetPasswordModelView tempResetpass { get; set; }

        [TempData]
        public string TempDataSuccededPassWordChange { get; set; }

        public ResetPasswordPageModel(UserManager<UserModel> userManager, IMangaRepository mangaRepository, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this.mangaRepository = mangaRepository;
            this.roleManager = roleManager;
        }

        [BindProperty]
        public ResetPasswordModelView model { get; set; }

        public async Task<IActionResult> OnPost(ResetPasswordModelView resetPasswordModelView)
        {

            UserModel activeuser = await _userManager.GetUserAsync(User);

            model.Email = resetPasswordModelView.Email;
            model.Token = resetPasswordModelView.Token;
            if (model.Password == null)
            {
                GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(activeuser);
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }
            if (model.ConfirmPassword == null)
            {
                GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(activeuser);
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordModelView.Email);
            if (user == null || await _userManager.IsInRoleAsync(user, "ShadowBanned") || await _userManager.IsInRoleAsync(user, "DELETEDUSER"))
            {
                return RedirectToPage("/Pages/Login/ForgotPassword");
            }

            var resetPassResult = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
            if (!resetPassResult.Succeeded)
            {
                foreach (var error in resetPassResult.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);
                }
                TempDataSuccededPassWordChange = "Password Changed Successfully, Login as usual";
                GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(activeuser);
                AllBooksList = GetAllBooks.ToList();
                return Page();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> OnGetAsync(ResetPasswordModelView resetPasswordModel)
        {
            tempResetpass = new ResetPasswordModelView { Token = resetPasswordModel.Token, Email = resetPasswordModel.Email };
            UserModel activeuser = await _userManager.GetUserAsync(User);
            GetAllBooks = await mangaRepository.Get10MangaEssentialMangaDtoIncludedAsync(activeuser);
            AllBooksList = GetAllBooks.ToList();
            return Page();
        }
    }
}