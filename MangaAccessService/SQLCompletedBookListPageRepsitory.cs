using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLCompletedBookListPageRepsitory : ICompletedBookListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLCompletedBookListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public CompletedBookList Add(CompletedBookList addNewModel)
        {
            context.completedBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public CompletedBookList Delete(int id)
        {
            CompletedBookList modelToDelete = context.completedBookLists.Find(id);
            if (modelToDelete != null)
            {
                context.completedBookLists.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<CompletedBookList> GetAllModels()
        {
            return context.completedBookLists;
        }

        public CompletedBookList GetModel(int id)
        {
            return context.completedBookLists.Find(id);
        }

        public CompletedBookList Update(CompletedBookList UpdateModel)
        {
            var currentModel = context.completedBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<CompletedBookList> AddAsync(CompletedBookList AddAsync)
        {
            await context.completedBookLists.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<CompletedBookList> DeleteAsync(int idAsync)
        {
            CompletedBookList modelToDelete = await context.completedBookLists.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.completedBookLists.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<CompletedBookList>> GetAllModelAsync()
        {
            return await context.completedBookLists.ToListAsync();
        }

        public async Task<CompletedBookList> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.completedBookLists.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<CompletedBookList> UpdateAsync(CompletedBookList UpdateModelAsync)
        {
            var modelUpdate = context.completedBookLists.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}