using MangaAccessService;
using MangaModelService;
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
        public MangaModel _MangaModel { get; set; }

        [BindProperty]
        public IFormFile? Photo { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime ReleaseYear { get; set; } = DateTime.Now;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM}", ApplyFormatInEditMode = true)]
        [BindProperty]
        public DateTime? EndingYear { get; set; } = null;

        [BindProperty]
        public List<int> SelectedTags { get; set; }

        public IEnumerable<TagModel> Tags { get; set; }
        public IEnumerable<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }

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
            // Check if the uploads directory exists, and create it if it does not
            var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }
            string photoPath;
            if (Photo != null)
            {
                photoPath = ProcessUploadedFile();
            }
            else
            {
                photoPath = null;
            }
            Tags = context.TagModels.ToList();
            var newMangaModel = context.mangaModels.FirstOrDefault(mm => mm.MangaName == _MangaModel.MangaName);
            //var ListOfAssiocatedNames = names
            var selectedTagIds = SelectedTags ?? new List<int>();
            // Add new tags that are selected now
            _MangaModel.TagsModels = new List<TagModel>();
            foreach (var tagId in selectedTagIds)
            {
                var tagToAdd = context.TagModels.Find(tagId);
                if (tagToAdd != null)
                {
                    _MangaModel.TagsModels.Add(tagToAdd);
                }
            }
            if (newMangaModel == null)
            {
                MangaModel MangaModels = new MangaModel
                {
                    MangaName = _MangaModel.MangaName,

                    AssociatedNames = null,
                    PhotoPath = photoPath,

                    BlogModel = new BlogModel { mangaName = _MangaModel.MangaName },
                    GroupScanlating = null,
                    GroupScanlatingID = null,
                    userModels = null,
                    userId = null,
                    futureEvents = _MangaModel.futureEvents,
                    AllLanguages = null,
                    score = 10,
                    StatusInCountryOfOrigin = _MangaModel.StatusInCountryOfOrigin,
                    relatedSeries = null,
                    ISBN10 = _MangaModel.ISBN10,
                    ISBN13 = _MangaModel.ISBN13,
                    Authormodels = null,
                    ArtistModels = null,
                    Description = _MangaModel.Description,
                    ReleaseYear = ReleaseYear,
                    EndingYear = EndingYear,
                    Type = _MangaModel.Type,
                    OfficalLanguage = _MangaModel.OfficalLanguage,
                    OriginalPublisher = _MangaModel.orignalWebtoon,
                    CompletelyTranslated = _MangaModel.CompletelyTranslated,
                    WeekRead = 0,
                    MonthRead = 0,
                    YearRead = 0,
                    StudioModels = null,
                    reviews = null,
                    BuyPages = null,
                    OfficalWebsites = null,
                    RecommendedMangaModels = null,
                    orignalWebtoon = _MangaModel.orignalWebtoon,
                    TagsModels = _MangaModel.TagsModels,
                    Characters = null,
                    GenresModels = null,
                    BookAddedToDB = DateTime.Now,
                    VoiceActors = null,
                };

                context.mangaModels.Add(MangaModels);
                context.SaveChanges();
                SucessFulManga = "Manga has been created successfully";

                return RedirectToPage("/Index");
            }
            return RedirectToPage("/Index");

            Tags = tagRepsitory.GetAllModels();
            MangaModels = mangaRepository.GetAllManga();
            return Page();
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Tags = await context.TagModels.ToListAsync();
            MangaModels = mangaRepository.GetAllManga();

            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage");
                string extension = Path.GetExtension(Photo.FileName);
                uniqueFileName = Guid.NewGuid().ToString() + extension;
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