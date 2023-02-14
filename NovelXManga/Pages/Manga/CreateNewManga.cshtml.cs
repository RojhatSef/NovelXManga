using MangaAccessService;
using MangaModelService;
using MangaModelService.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace NovelXManga.Pages.Manga
{
    [Authorize]
    public class Create_new_MangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagRepsitory tagRepsitory;

        [BindProperty]
        public MangaModelView _MangaModel { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [BindProperty]
        [DataType(DataType.Date)]
        public DateTime ReleaseYear { get; set; } = DateTime.Now;

        [BindProperty]
        public List<int> SelectedChanges { get; set; } = new List<int>();

        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel>MangaModels { get; set;  }

        [TempData]
        public string SucessFulManga { get; set; }

        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, ITagRepsitory tagRepsitory)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.tagRepsitory = tagRepsitory;
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
                // Check if the uploads directory exists, and create it if it does not
                var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsDirectory))
                {
                    Directory.CreateDirectory(uploadsDirectory);
                }
                string photoPath = null;
                if (Photo != null)
                {
                    // Save the uploaded photo to a file on the server
                    var filePath = Path.Combine(webHostEnvironment.WebRootPath, "uploads", Photo.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        Photo.CopyTo(stream);
                    }
                    photoPath = Photo.FileName;
                }
                var newMangaModel = context.mangaModels.FirstOrDefault(mm => mm.MangaName == _MangaModel.MangaName);
                //var ListOfAssiocatedNames = names
                var selectedTagLogs = Tags?.Where(x => SelectedChanges?.Contains(x.TagId) ?? false).ToList();
                if (newMangaModel == null)
                {
                    MangaModel MangaModels = new MangaModel
                    {
                        MangaName = _MangaModel.MangaName,
                        //AssociatedNames = new List<AssociatedNames> { ListOfAssiocatedNames },
                        AssociatedNames = null,
                        PhotoPath = photoPath,
                        ReleaseYear = ReleaseYear,
                        BlogModel = new BlogModel { mangaName = _MangaModel.MangaName },
                        GroupScanlating = null,
                        GroupScanlatingID = null,
                        userModels = null,
                        userId = null,
                        futureEvents = null,
                        AllLanguages = null,
                        score = 10,
                        StatusInCountryOfOrigin = "Unknown",
                        relatedSeries = null,
                        ISBN10 = null,
                        ISBN13 = null,
                        Authormodels = null,
                        ArtistModels = null,
                    };

                    context.mangaModels.Add(MangaModels);
                    context.SaveChanges();
                    SucessFulManga = "Manga has been created successfully";

                    return RedirectToPage("/Index");
                }
                return RedirectToPage("/Index");
            }
            Tags = tagRepsitory.GetAllModels();
            MangaModels = mangaRepository.GetAllManga();
            return Page();
        }

        public async Task OnGet()
        {
            Tags = await context.TagModels.ToListAsync();
            MangaModels = mangaRepository.GetAllManga(); 

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

#endregion TestOnGet