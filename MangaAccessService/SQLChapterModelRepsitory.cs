using MangaModelService;

namespace MangaAccessService
{
    public class SQLChapterModelRepsitory : IChapterModelRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;
        public SQLChapterModelRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public ChapterModel Add(ChapterModel addNewModel)
        {
            context.chapterModels.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public ChapterModel Delete(string id)
        {
            ChapterModel chaptoDelete = context.chapterModels.Find(id);
            if (chaptoDelete != null)
            {
                context.chapterModels.Remove(chaptoDelete);
                context.SaveChanges();

            }
            return chaptoDelete;
        }

        public IEnumerable<ChapterModel> GetAllModels()
        {
            return context.chapterModels;
        }

        public ChapterModel GetModel(string id)
        {
            return context.chapterModels.Find(id);
        }

        public IEnumerable<ChapterModel> Search(string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm))
            {
                return context.chapterModels;
            }
            return context.chapterModels.Where(e => e.chapterName.Contains(searchTerm) || e.chapterLinkNumber.Contains(searchTerm));
        }

        public ChapterModel Update(ChapterModel UpdateModel)
        {
            var currentModel = context.chapterModels.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }
    }
}
