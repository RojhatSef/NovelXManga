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
    [Authorize(Roles = "Owner, Admin, Updater, Publisher, Artist, Author, Tester, AdminControl, Scanlators, BackEndDeveloper, SupportGroups")]
    public class Create_new_MangaModel : PageModel
    {
        private readonly MangaNNovelAuthDBContext context;
        private readonly IMangaRepository mangaRepository;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly ITagRepsitory tagRepsitory;

        [BindProperty]
        public MangaModel _MangaModel { get; set; }

        [BindProperty]
        public AuthorViewModel authorViewModel { get; set; }

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
        [FromForm(Name = "selectedTags")]
        public List<int> SelectedTags { get; set; }

        [BindProperty]
        [FromForm(Name = "selectedGenres")]
        public List<int> SelectedGenres { get; set; }

        public List<TagModel> Tags { get; set; }
        public List<GenresModel> Genres { get; set; }
        public IEnumerable<MangaModel> MangaModels { get; set; }

        [TempData]
        public string SucessFulManga { get; set; }

        public Create_new_MangaModel(MangaNNovelAuthDBContext mangaNNovelAuthDBContext, IMangaRepository mangaRepository, IWebHostEnvironment webHostEnvironment, ITagRepsitory tagRepsitory, ILogger<IndexModel> logger)
        {
            this.context = mangaNNovelAuthDBContext;
            this.mangaRepository = mangaRepository;
            this.webHostEnvironment = webHostEnvironment;
            this.tagRepsitory = tagRepsitory;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // Check if the uploads directory exists, and create it if it does not

            var uploadsDirectory = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage");
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            // Process uploaded file and get the photo path, if there's no photo photopath is null.
            string photoPath = Photo != null ? ProcessUploadedFile() : null;
            // create a new list of TagModel objects
            var selectedTags = await context.TagModels.Where(tag => SelectedTags.Contains(tag.TagId)).ToListAsync();
            var selectedGenres = await context.GenresModels.Where(tag => SelectedGenres.Contains(tag.GenresId)).ToListAsync();

            _MangaModel.TagsModels = selectedTags;

            _MangaModel.PhotoPath = photoPath;
            _MangaModel.ReleaseYear = ReleaseYear;
            _MangaModel.EndingYear = EndingYear;
            _MangaModel.TagsModels = selectedTags;
            _MangaModel.GenresModels = selectedGenres;

            #region If Something == null

            if (_MangaModel.MangaName == null)
            {
                SucessFulManga = "Book failed to create!";
                return RedirectToPage("/Index");
            }

            #endregion If Something == null

            MangaModel MangaModels = new MangaModel
            {
                MangaName = _MangaModel.MangaName,
                PhotoPath = _MangaModel.PhotoPath,
                BlogModel = new BlogModel { mangaName = _MangaModel.MangaName },
                futureEvents = _MangaModel.futureEvents,

                StatusInCountryOfOrigin = _MangaModel.StatusInCountryOfOrigin,
                ISBN10 = _MangaModel.ISBN10,
                ISBN13 = _MangaModel.ISBN13,
                Description = _MangaModel.Description,
                ReleaseYear = _MangaModel.ReleaseYear,
                EndingYear = EndingYear,
                Type = _MangaModel.Type,
                OfficalLanguage = _MangaModel.OfficalLanguage,
                OriginalPublisher = _MangaModel.orignalWebtoon,
                CompletelyTranslated = _MangaModel.CompletelyTranslated,

                orignalWebtoon = _MangaModel.orignalWebtoon,
                TagsModels = selectedTags,
                GenresModels = selectedGenres,
                BookAddedToDB = DateTime.UtcNow,
            };

            await context.mangaModels.AddAsync(MangaModels);
            await context.SaveChangesAsync();
            int newMangaId = MangaModels.MangaID;
            SucessFulManga = "Manga has been created successfully";
            return RedirectToPage("/Author/CreateAuthor", new { mangaId = newMangaId });
        }

        public async Task<IActionResult> OnGetAsync()
        {
            Tags = await context.TagModels.ToListAsync();
            Genres = await context.GenresModels.ToListAsync();
            MangaModels = await mangaRepository.GetAllModelAsync();

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