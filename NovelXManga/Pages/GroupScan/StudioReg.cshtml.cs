using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.GroupScan
{

    public class StudioRegModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        public StudioRegModel(IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, UserManager<IdentityUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public StudioModel studioModel { get; set; }

        [BindProperty]
        public string MangaName { get; set; }

        public IdentityUser Users { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id != null)
            {
                var groupUser = await userManager.GetUserAsync(User);
                Users = groupUser;

            }
            return Page();

        }
        public async Task<IActionResult> OnPostAsync()
        {

            if (ModelState.IsValid)
            {
                // get current user when creating a new group
                var currentUser = await userManager.GetUserAsync(User);
                UserModel currentUserModel = (UserModel)currentUser;
                var getMasterManga = mangaNNovelAuthDBContext.MasterModels.FirstOrDefault(mm => mm.MangaModels.MangaName == MangaName);
                var newGroup = mangaNNovelAuthDBContext.studioModels.FirstOrDefault(gcm => gcm.GroupName == studioModel.GroupName);
                if (newGroup == null)
                {
                    var NewScanGroup = new StudioModel()
                    {
                        GroupName = studioModel.GroupName,
                        chapterModels = null,
                        PhotoPath = ProcessUploadedFile(),
                        website = studioModel.website,
                        MasterModels = new List<MasterModel> { getMasterManga },
                        userModels = new List<UserModel> { currentUserModel }

                    };
                    mangaNNovelAuthDBContext.groupScanlatingModels.Add(NewScanGroup);
                    mangaNNovelAuthDBContext.SaveChanges();

                }

                return RedirectToPage("/Index");
            }
            return Page();
        }
        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "images/GroupImages");
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
