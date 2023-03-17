using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLFavoritListPageRepsitory : IFavoritListPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLFavoritListPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public FavoritBookList Add(FavoritBookList addNewModel)
        {
            context.favoritBookLists.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public FavoritBookList Delete(int id)
        {
            FavoritBookList chaptoDelete = context.favoritBookLists.Find(id);
            if (chaptoDelete != null)
            {
                context.favoritBookLists.Remove(chaptoDelete);
                context.SaveChanges();
            }
            return chaptoDelete;
        }

        public IEnumerable<FavoritBookList> GetAllModels()
        {
            return context.favoritBookLists;
        }

        public FavoritBookList GetModel(int id)
        {
            return context.favoritBookLists.Find(id);
        }

        public FavoritBookList Update(FavoritBookList UpdateModel)
        {
            var currentModel = context.favoritBookLists.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<FavoritBookList> AddAsync(FavoritBookList AddAsync)
        {
            await context.favoritBookLists.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<FavoritBookList> DeleteAsync(int idAsync)
        {
            FavoritBookList modelToDelete = await context.favoritBookLists.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.favoritBookLists.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<FavoritBookList>> GetAllModelAsync()
        {
            return await context.favoritBookLists.ToListAsync();
        }

        public async Task<FavoritBookList> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.favoritBookLists.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<FavoritBookList> UpdateAsync(FavoritBookList UpdateModelAsync)
        {
            var modelUpdate = context.favoritBookLists.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}