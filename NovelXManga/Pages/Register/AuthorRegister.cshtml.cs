using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Register
{
    public class AuthorRegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly MangaNNovelAuthDBContext nNovelAuthDBContext;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public AuthorModel Authormodel { get; set; }
        [BindProperty]
        public RegisterModel RegiModel { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }


        public AuthorRegisterModel(UserManager<IdentityUser> userManager, MangaNNovelAuthDBContext nNovelAuthDBContext, SignInManager<IdentityUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.nNovelAuthDBContext = nNovelAuthDBContext;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;
        }


        public void onGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (Authormodel.userPhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage", Authormodel.userPhotoPath);
                    if (!filePath.EndsWith("NoPhoto.png")) { System.IO.File.Delete(filePath); }
                }
                var user = new AuthorModel()
                {
                    UserName = RegiModel.Email,
                    Email = RegiModel.Email,
                    FirstName = Authormodel.FirstName,
                    LastName = Authormodel.LastName,
                    userPhotoPath = ProcessUploadedFile(),

                };
                var result = await userManager.CreateAsync(user, RegiModel.Password);
                if (result.Succeeded)
                {
                    await signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToPage("/Index");

                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "images/AuthorImage");
                uniqueFileName = Guid.NewGuid().ToString() + ".png";
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
    }
}
