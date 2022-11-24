using MangaAccessService;
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
        public async Task<IActionResult> OnGet(string id)
        {
            if (id != null)
            {
                var groupUser = await userManager.GetUserAsync(User);
                var userId = groupUser?.Id;

            }
            return Page();

        }
        //public IActionResult OnPostAsync()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newGroup = mangaNNovelAuthDBContext.groupScanlatingModels.FirstOrDefault(gcm => gcm.GroupName == groupModelView.GroupName);
        //        if (newGroup != null)
        //        {
        //            GroupScanlatingModel groupScanlatingModel = new GroupScanlatingModel()
        //            {
        //                GroupName = groupModelView.GroupName,
        //                chapterModels = null,
        //            }
        //        }
        //    }
        //}
    }
}
