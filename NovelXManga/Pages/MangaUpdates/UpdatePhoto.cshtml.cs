using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace NovelXManga.Pages.MangaUpdates
{
    public class UpdatePhotoModel : PageModel
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly MangaNNovelAuthDBContext _context;
        private readonly IMangaRepository mangaRepository;

        public UpdatePhotoModel(IWebHostEnvironment webHostEnvironment, IMangaRepository mangaRepository, MangaNNovelAuthDBContext context)
        {
            this.webHostEnvironment = webHostEnvironment;

            this.mangaRepository = mangaRepository;
            _context = context;
        }

        [BindProperty(Name = "Photo")]
        public IFormFile Photo { get; set; }

        [BindProperty]
        public MangaModel mangaModelUpdate { get; set; }

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

        public async Task<IActionResult> OnPostAsync(MangaModel mangaModel)
        {
            if (mangaModel == null)
            {
                return NotFound();
            }
            mangaModelUpdate = mangaRepository.GetOneMangaAllIncluded(mangaModel.MangaID);

            if (mangaModelUpdate.PhotoPath != null)
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", "MangaImage", mangaModelUpdate.PhotoPath);
                if (System.IO.File.Exists(filePath) && !filePath.EndsWith("NoPhoto.png"))
                {
                    try
                    {
                        System.IO.File.Delete(filePath);
                    }
                    catch (Exception ex)
                    {
                        // Handle any exceptions that may occur
                        ModelState.AddModelError("Photo", ex.Message);
                        return Page();
                    }
                }
            }

            mangaModelUpdate.PhotoPath = ProcessUploadedFile();
            mangaModelUpdate = mangaRepository.Update(mangaModelUpdate);

            return RedirectToPage("/Index");
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mangaModelUpdate = _context.mangaModels.FirstOrDefault(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}