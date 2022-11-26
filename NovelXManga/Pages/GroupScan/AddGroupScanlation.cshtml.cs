using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.GroupScan
{
    public class AddGroupScanlationModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly UserManager<IdentityUser> userManager;
        public AddGroupScanlationModel(IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, UserManager<IdentityUser> userManager)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.userManager = userManager;
        }
        [BindProperty]
        public GroupScanlationModelView groupModelView { get; set; }

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
                var newGroup = mangaNNovelAuthDBContext.groupScanlatingModels.FirstOrDefault(gcm => gcm.GroupName == groupModelView.GroupName);
                if (newGroup == null)
                {
                    var NewScanGroup = new GroupScanlatingModel()
                    {
                        GroupName = groupModelView.GroupName,
                        chapterModels = null,
                        PhotoPath = ProcessUploadedFile(),
                        website = groupModelView.website,
                        MasterModels = new List<MasterModel> { getMasterManga },
                        userModels = new List<UserModel> { currentUserModel }

                    };

                    mangaNNovelAuthDBContext.groupScanlatingModels.Add(NewScanGroup);
                    mangaNNovelAuthDBContext.SaveChanges();

                }

                return RedirectToPage("Index");
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
