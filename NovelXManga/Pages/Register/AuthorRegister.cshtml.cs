using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NovelXManga.Pages.Register
{
    [Authorize]
    public class AuthorRegisterModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly MangaNNovelAuthDBContext nNovelAuthDBContext;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public AuthorModel Authormodel { get; set; }
        [BindProperty]
        public ArtistModel Artistmodel { get; set; }
        [BindProperty]
        public VoiceActorModel VoiceActor { get; set; }
        [BindProperty]
        public AuthorViewModel authorViewModel { get; set; }
        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        [EnumDataType(typeof(Creator))]
        public Creator Creators { get; set; }
        public AuthorRegisterModel(UserManager<IdentityUser> userManager, MangaNNovelAuthDBContext nNovelAuthDBContext, SignInManager<IdentityUser> signInManager, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.nNovelAuthDBContext = nNovelAuthDBContext;
            this.signInManager = signInManager;
            this.webHostEnvironment = webHostEnvironment;



        }
        public enum Creator
        {
            Voice = 1,
            Artist = 2,
            Author = 3
        }


        public void onPost()
        {
            if (Creators.Equals(3))
            {
                if (ModelState.IsValid)
                {
                    Authormodel = new AuthorModel
                    {
                        FirstName = authorViewModel.FirstName,
                        LastName = authorViewModel.LastName,
                        AssociatedNames = authorViewModel.AssociatedNames,
                        NameInNative = authorViewModel.NameInNative,
                    };
                }
            }
            else if (Creators.Equals(2))
            {
                if (ModelState.IsValid)
                {
                    Artistmodel = new ArtistModel
                    {
                        FirstName = authorViewModel.FirstName,
                        LastName = authorViewModel.LastName,
                        AssociatedNames = authorViewModel.AssociatedNames,
                        NameInNative = authorViewModel.NameInNative,
                    };
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    VoiceActor = new VoiceActorModel
                    {
                        FirstName = authorViewModel.FirstName,
                        LastName = authorViewModel.LastName,
                        AssociatedNames = authorViewModel.AssociatedNames,
                        NameInNative = authorViewModel.NameInNative,
                    };
                }
            }




        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder =
                    Path.Combine(webHostEnvironment.WebRootPath, "images/AuthorImage");
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
