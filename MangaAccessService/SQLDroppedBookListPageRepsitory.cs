using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLDroppedBookListPageRepsitory : IDroppedBookListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLDroppedBookListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public DroppedBookList Add(DroppedBookList addNewModel)
        {
            context.droppedBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public DroppedBookList Delete(int id)
        {
            DroppedBookList chaptoDelete = context.droppedBookLists.Find(id);
            if (chaptoDelete != null)
            {
                context.droppedBookLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<DroppedBookList> GetAllModels()
        {
            return context.droppedBookLists;
        }

        public DroppedBookList GetModel(int id)
        {
            return context.droppedBookLists.Find(id);
        }

        public DroppedBookList Update(DroppedBookList UpdateModel)
        {
            var currentModel = context.droppedBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<DroppedBookList> AddAsync(DroppedBookList AddAsync)
        {
            await context.droppedBookLists.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<DroppedBookList> DeleteAsync(int idAsync)
        {
            DroppedBookList modelToDelete = await context.droppedBookLists.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.droppedBookLists.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<DroppedBookList>> GetAllModelAsync()
        {
            return await context.droppedBookLists.ToListAsync();
        }

        public async Task<DroppedBookList> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.droppedBookLists.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<DroppedBookList> UpdateAsync(DroppedBookList UpdateModelAsync)
        {
            var modelUpdate = context.droppedBookLists.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}