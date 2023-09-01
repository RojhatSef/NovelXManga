using MangaAccessService;
using Microsoft.EntityFrameworkCore;

namespace NovelXManga
{
    public static class DatabaseUtility
    {
        private static IWebHostEnvironment webHostEnvironment;

        public static void Initialize(IWebHostEnvironment _webHostEnvironment)
        {
            webHostEnvironment = _webHostEnvironment;
        }

        public static void ResetDatabaseAndCleanImages(MangaNNovelAuthDBContext context)
        {
            DropAllTables(context);

            // Deleting generated images
            DeleteGeneratedImages("GeneratedCharacterImage");
            DeleteGeneratedImages("GeneratedWallpaperImages");
            DeleteGeneratedImages("GeneratedMangaImage");
            // ... add others as needed
        }

        public static void DropAllTables(DbContext context)
        {
            // ... (existing DropAllTables code)
        }

        public static void DeleteGeneratedImages(string generatedFolder)
        {
            string directoryPath = Path.Combine(webHostEnvironment.WebRootPath, "Images", generatedFolder);
            DirectoryInfo directory = new DirectoryInfo(directoryPath);
            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }
        }
    }
}