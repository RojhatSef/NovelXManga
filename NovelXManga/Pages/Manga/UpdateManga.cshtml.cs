using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Manga
{
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Updater")]
    [Authorize(Roles = "Admin")]
    [Authorize(Roles = "Owner")]
    [Authorize(Roles = "AdminControl")]


    public class UpdateMangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly UserManager<UserModel> userManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        public IEnumerable<MangaModel> mangaModel { get; set; }
        public UpdateMangaModel(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }



    }
}
