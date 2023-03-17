using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Author
{
    public class CreateAuthorModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        public IEnumerable<MangaModel> MangaModels { get; set; }

        public CreateAuthorModel(MangaNNovelAuthDBContext context, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }

        public void OnPostAuthor(List<AuthorModel> model)
        {
            //do your stuff...
        }

        public IActionResult OnGet()
        {
            MangaModels = mangaRepository.GetAllManga();

            return Page();
        }
    }
}