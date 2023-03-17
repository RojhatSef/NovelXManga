using MangaModelService;
using Microsoft.EntityFrameworkCore;

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

        public ChapterModel Delete(int id)
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

        public ChapterModel GetModel(int id)
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

        public async Task<ChapterModel> AddAsync(ChapterModel AddAsync)
        {
            await context.chapterModels.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<ChapterModel> DeleteAsync(int idAsync)
        {
            ChapterModel modelToDelete = await context.chapterModels.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.chapterModels.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<ChapterModel>> GetAllModelAsync()
        {
            return await context.chapterModels.ToListAsync();
        }

        public async Task<ChapterModel> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.chapterModels.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<ChapterModel> UpdateAsync(ChapterModel UpdateModelAsync)
        {
            var modelUpdate = context.chapterModels.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<IEnumerable<ChapterModel>> SearchAsync(string searchTermAsync)
        {
            if (string.IsNullOrEmpty(searchTermAsync))
            {
                return await context.chapterModels.ToListAsync();
            }
            return await context.chapterModels.Where(e => e.chapterName.Contains(searchTermAsync) || e.chapterLinkNumber.Contains(searchTermAsync)).ToListAsync(); ;
        }
    }
}