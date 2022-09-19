using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Register
{
    public class ALlAuthorsModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;

        public IEnumerable<AuthorModel> authorModel { get; set; }
        public ALlAuthorsModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext)
        {
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
        }
        public void OnGet()
        {
            authorModel = mangaNNovelAuthDBContext.authorModels;
        }
    }
}
