using MangaAccessService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Voice
{
    public class AllVoiceModel : PageModel
    {
        private readonly IVoiceRepsitory voiceRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AllVoiceModel(IWebHostEnvironment webHostEnvironment = null, IVoiceRepsitory voiceRepsitory = null)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.voiceRepsitory = voiceRepsitory;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return Page();
        }
    }
}