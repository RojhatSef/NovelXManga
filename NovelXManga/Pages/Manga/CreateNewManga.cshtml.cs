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
        public IFormFile? Photo { get; set; }

        [BindProperty]
        public MangaModelView MangaModelView { get; set; }
        [BindProperty]

        [DataType(DataType.Date)]
        public DateTime ReleaseYear { get; set; } = DateTime.Now;


        [TempData]
        public string SucessFulManga { get; set; }






        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment)
        {
            this.mangaNNovelAuthDBContext = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
        }
        public AssociatedNames names(string AssociatedNames)
        {
            AssociatedNames associatedNames = new AssociatedNames();
            associatedNames.nameString = AssociatedNames;

            return associatedNames;
        }

        public IActionResult OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                //if (Photo != null)
                //{
                //    string filePath = Path.Combine(webHostEnvironment.WebRootPath, "images/MangaImage", mangaModelView.PhotoPath);
                //    if (!filePath.EndsWith("NoPhoto.png")) { System.IO.File.Delete(filePath); }
                //}
                var newMangaModel = mangaNNovelAuthDBContext.mangaModels.FirstOrDefault(mm => mm.MangaName == MangaModelView.MangaName);
                var ListOfAssiocatedNames = names(mangaModelView.AssociatedNames);
                if (newMangaModel == null)
                {



                    MangaModel MangaModels = new MangaModel
                    {
                        MangaName = mangaModelView.MangaName,
                        AssociatedNames = new List<AssociatedNames> { ListOfAssiocatedNames },
                        PhotoPath = ProcessUploadedFile(),
                        ReleaseYear = ReleaseYear,
                        BlogModel = new BlogModel { mangaName = mangaModelView.MangaName },
                        GroupScanlating = null,
                        GroupScanlatingID = null,
                        userModels = null,
                        userId = null,

                    };


                    mangaNNovelAuthDBContext.mangaModels.Add(MangaModels);
                    mangaNNovelAuthDBContext.SaveChanges();
                    SucessFulManga = "Manga has been created successfully";

                    return RedirectToPage("/Index");
                }
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
//            MasterID = mangaModel.MasterModelID,



//        };

//    }

//    if (mangaModel == null)
//    {
//        return RedirectToPage("/Index");
//    }
//    return Page();
//}
#endregion