using MangaAccessService;
using MangaModelService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga.Pages.MangaUpdates
{
    [Authorize(Roles = "Owner, Admin, Updater")]
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
                string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "Images", "GeneratedMangaImage");
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
            mangaModelUpdate = await _context.mangaModels.FirstOrDefaultAsync(e => e.MangaID == mangaModel.MangaID);

            if (mangaModelUpdate.PhotoPath != null)
            {
                string filePath = Path.Combine(webHostEnvironment.WebRootPath, "Images", "GeneratedMangaImage", mangaModelUpdate.PhotoPath);
                if (System.IO.File.Exists(filePath))
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
            mangaModelUpdate = await mangaRepository.UpdateAsync(mangaModelUpdate);

            return RedirectToPage("/Manga/CurrentManga", new { id = mangaModelUpdate.MangaID });
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            mangaModelUpdate = await _context.mangaModels.FirstOrDefaultAsync(e => e.MangaID == id);

            if (mangaModelUpdate == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}