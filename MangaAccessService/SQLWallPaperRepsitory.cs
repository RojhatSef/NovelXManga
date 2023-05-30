using MangaModelService;
using Microsoft.EntityFrameworkCore;

namespace MangaAccessService
{
    public class SQLWallPaperRepsitory : IWallPaperRepsitory
    {
        private readonly MangaNNovelAuthDBContext context;

        public SQLWallPaperRepsitory(MangaNNovelAuthDBContext context)
        {
            this.context = context;
        }

        public WallPapers Add(WallPapers addNewModel)
        {
            context.WallPapers.Add(addNewModel);
            context.SaveChanges();
            return addNewModel;
        }

        public WallPapers Delete(int id)
        {
            WallPapers modelToDelete = context.WallPapers.Find(id);
            if (modelToDelete != null)
            {
                context.WallPapers.Remove(modelToDelete);
                context.SaveChanges();
            }
            return modelToDelete;
        }

        public IEnumerable<WallPapers> GetCharactersByIds(List<int> characterIds)
        {
            return context.WallPapers.Where(c => characterIds.Contains(c.WallpaperID));
        }

        public IEnumerable<WallPapers> GetAllModels()
        {
            return context.WallPapers;
        }

        public WallPapers GetModel(int id)
        {
            return context.WallPapers.Find(id);
        }

        public WallPapers Update(WallPapers UpdateModel)
        {
            var currentModel = context.WallPapers.Attach(UpdateModel);
            currentModel.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return UpdateModel;
        }

        public async Task<WallPapers> AddAsync(WallPapers AddAsync)
        {
            await context.WallPapers.AddAsync(AddAsync);
            await context.SaveChangesAsync();
            return AddAsync;
        }

        public async Task<WallPapers> DeleteAsync(int idAsync)
        {
            WallPapers modelToDelete = await context.WallPapers.FindAsync(idAsync);
            if (modelToDelete != null)
            {
                context.WallPapers.Remove(modelToDelete);
                await context.SaveChangesAsync();
            }
            return modelToDelete;
        }

        public async Task<IEnumerable<WallPapers>> GetAllModelAsync()
        {
            return await context.WallPapers.ToListAsync();
        }

        public async Task<WallPapers> GetModelAsync(int idAsync)
        {
            var CurrentModel = await context.WallPapers.FindAsync(idAsync);
            return CurrentModel;
        }

        public async Task<WallPapers> UpdateAsync(WallPapers UpdateModelAsync)
        {
            var modelUpdate = context.WallPapers.Attach(UpdateModelAsync);
            modelUpdate.State = EntityState.Modified;
            await context.SaveChangesAsync();
            return UpdateModelAsync;
        }
    }
}