using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace NovelXManga.Pages.Manga
{
    [Authorize]
    public class Create_new_MangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext mangaNNovelAuthDBContext;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;

        [BindProperty]
        public MangaModelView mangaModelView { get; set; }


        [BindProperty]
        public MangaModelView MangaModelView { get; set; }
        [BindProperty]
        public DateTime ReleaseYear { get; set; } = DateTime.Now;
        [DataType(DataType.Date)]

        [BindProperty]
        public MasterModel MasterModel { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }



        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }


        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                if (mangaModelView.PhotoPath != null)
                {
                    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage", mangaModelView.PhotoPath);
                    if (!filePath.EndsWith("NoPhoto.png")) { System.IO.File.Delete(filePath); }
                }
                var newBlogModel = new BlogModel { mangaName = mangaModelView.MangaName };
                var newmasterModel = new MasterModel
                {
                    MangaModels = new MangaModel
                    {
                        MangaName = mangaModelView.MangaName,
                        AssociatedNames = mangaModelView.AssociatedNames,
                        PhotoPath = ProcessUploadedFile(),
                        ReleaseYear = ReleaseYear,
                        BlogModel = newBlogModel,
                    },
                };
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
                    Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage");
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
#region TestOnGet
//public IActionResult OnGet(int? id)
//{
//    if (id.HasValue)
//    {
//        mangaModel = mangaRepository.GetManga(id.Value);
//    }
//    else
//    {
//        var newManga = new MangaModel
//        {
//            MangaName = mangaModel.MangaName,
//            AssociatedNames = mangaModel.AssociatedNames,
//            MangaID = mangaModel.MangaID,
//            MasterModelID = mangaModel.MasterModelID,



//        };
//    }

//    if (mangaModel == null)
//    {
//        return RedirectToPage("/Index");
//    }
//    return Page();
//}
#endregion