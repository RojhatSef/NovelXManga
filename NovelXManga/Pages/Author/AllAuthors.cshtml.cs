using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Author
{
    public class AllAuthorsModel : PageModel
    {

        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
        private readonly IAuthorRepsitory authorRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IEnumerable<AuthorModel> AuthorModels { get; set; }
        public AllAuthorsModel(IAuthorRepsitory authorRepsitory, MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IWebHostEnvironment webHostEnvironment)
        {
            this.authorRepsitory = authorRepsitory;
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnGet(int id)
        {
            IEnumerable<AuthorModel> AuthorList = authorRepsitory.GetAuthorModels();
            AuthorModels = AuthorList;
        }
        //public Task<IActionResult> OnPost()
        //{
        //    var AuthorList
        //    return 
        //}
    }
}
