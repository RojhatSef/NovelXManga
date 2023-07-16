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
        private readonly UserManager<UserModel> userManager;

        public AddGroupScanlationModel(IWebHostEnvironment webHostEnvironment, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, UserManager<UserModel> userManager)
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
                var getMasterManga = mangaNNovelAuthDBContext.mangaModels.FirstOrDefault(mm => mm.MangaName == MangaName);
                var newGroup = mangaNNovelAuthDBContext.groupScanlatingModels.FirstOrDefault(gcm => gcm.GroupName == groupModelView.GroupName);
                if (newGroup == null)
                {
                    newGroup = new GroupScanlatingModel()
                    {
                        GroupName = groupModelView.GroupName,
                        chapterModels = null,
                        PhotoPath = ProcessUploadedFile(),
                        website = groupModelView.website,
                        userID = currentUser.Id,

                        userModels = new List<UserModel> { currentUserModel },
                    };

                    getMasterManga.GroupScanlating = new List<GroupScanlatingModel> { newGroup };
                    getMasterManga.GroupScanlatingID = newGroup.GroupScanlatingID;
                    getMasterManga.userId = currentUser.Id;
                    mangaNNovelAuthDBContext.groupScanlatingModels.Add(newGroup);
                    //                   mangaNNovelAuthDBContext.SaveChanges();

                    mangaNNovelAuthDBContext.mangaModels.Update(getMasterManga);
                    mangaNNovelAuthDBContext.SaveChanges();
                    //mastermodel does not update with groupscanlating model, why? When groupscanlatingmodel is complete that is when it recives an ID
                    // due to how EF works, it wont pass an id, so we need to update the masterModel again after the group has been created
                    // we can do this by taking in the latest input.
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