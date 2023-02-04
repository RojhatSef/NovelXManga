using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.Voice
{
    public class AllVoiceModel : PageModel
    {
        public IEnumerable<VoiceActorModel> CreatorModels { get; set; }
        private readonly IVoiceRepsitory voiceRepsitory;
        private readonly IWebHostEnvironment webHostEnvironment;

        public AllVoiceModel(IWebHostEnvironment webHostEnvironment = null, IVoiceRepsitory voiceRepsitory = null)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.voiceRepsitory = voiceRepsitory;
        }

        public void OnGet()
        {
            IEnumerable<VoiceActorModel> GetallModels = voiceRepsitory.GetAllModels(); ;
            CreatorModels = GetallModels;
        }
    }
}