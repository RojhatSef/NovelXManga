using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLReadingListPageRepsitory : IReadingListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLReadingListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public ReadingList Add(ReadingList addNewModel)
        {
            context.readingLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public ReadingList Delete(int id)
        {
            ReadingList chaptoDelete = context.readingLists.Find(id);
            if (chaptoDelete != null)
            {
                context.readingLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<ReadingList> GetAllModels()
        {
            return context.readingLists;
        }

        public ReadingList GetModel(int id)
        {
            return context.readingLists.Find(id);
        }

        public ReadingList Update(ReadingList UpdateModel)
        {
            var currentModel = context.readingLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<ReadingList> AddAsync(ReadingList AddAsync)
        {
            await context.readingLists.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<ReadingList> DeleteAsync(int idAsync)
        {
            ReadingList modelToDelete = await context.readingLists.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.readingLists.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<ReadingList>> GetAllModelAsync()
        {
            return await context.readingLists.ToListAsync();
        }

        public async Task<ReadingList> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.readingLists.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<ReadingList> UpdateAsync(ReadingList UpdateModelAsync)
        {
            var modelUpdate = context.readingLists.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }

        public async Task<int> GetReadingListCountForMangaAsync(int mangaId)
        {
            return await context.readingLists.CountAsync(rl => rl.MangaModelId == mangaId);
        }
    }
}