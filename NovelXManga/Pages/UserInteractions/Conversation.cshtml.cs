using MangaAccessService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.UserInteractions
{
    public class ConversationModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext Context;

        public ConversationModel(MangaNNovelAuthDBContext context)
        {
            Context = context;
        }

        public void OnGet()
        {
        }
    }
}