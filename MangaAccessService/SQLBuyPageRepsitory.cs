using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLBuyPageRepsitory : IBuyPageRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLBuyPageRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public BuyPage Add(BuyPage addNewModel)
        {
            context.BuyPages.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public BuyPage Delete(int id)
        {
            BuyPage modelToDelete = context.BuyPages.Find(id);
            if (modelToDelete != null)
            {
                context.BuyPages.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<BuyPage> GetAllModels()
        {
            return context.BuyPages;
        }

        public BuyPage GetModel(int id)
        {
            return context.BuyPages.Find(id);
        }

        public BuyPage Update(BuyPage UpdateModel)
        {
            var currentModel = context.BuyPages.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<BuyPage> AddAsync(BuyPage AddAsync)
        {
            await context.BuyPages.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<BuyPage> DeleteAsync(int idAsync)
        {
            BuyPage modelToDelete = await context.BuyPages.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.BuyPages.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<BuyPage>> GetAllModelAsync()
        {
            return await context.BuyPages.ToListAsync();
        }

        public async Task<BuyPage> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.BuyPages.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<BuyPage> UpdateAsync(BuyPage UpdateModelAsync)
        {
            var modelUpdate = context.BuyPages.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}